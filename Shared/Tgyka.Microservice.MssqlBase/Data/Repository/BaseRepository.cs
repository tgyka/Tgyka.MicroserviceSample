using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MssqlDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(MssqlDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity Get(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int skip = 0, int take = 0)
        {
            return Query(predicate, includes, orderBySelector, isDescending, skip, take).FirstOrDefault();
        }

        public IEnumerable<TEntity> List(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int skip = 0, int take = 0)
        {
            return Query(predicate, includes, orderBySelector, isDescending, skip, take).ToList();
        }

        public TEntity Set(TEntity entity, EntityState state)
        {
            _dbSet.Entry(entity).State = state;
            return entity;
        }

        public List<TEntity> Set(IEnumerable<TEntity> entitites, EntityState state)
        {
            foreach (var entry in entitites)
            {
                _dbSet.Entry(entry).State = state;

            }
            return entitites.ToList();
        }

        private IEnumerable<TEntity> Query(Func<TEntity, bool> predicate, List<Expression<Func<TEntity, object>>> includes,
            Func<TEntity, object> orderBySelector, bool isDescending, int skip, int take)
        {
            var query = _dbSet.AsNoTracking();
            query = query.Where(r => !r.IsDeleted);

            if (includes != null && includes.Count > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            IEnumerable<TEntity> queryPredicate = query;

            if (predicate != null)
            {
                queryPredicate = query.Where(predicate);
            }

            if (orderBySelector != null)
            {
                if (isDescending) queryPredicate = queryPredicate.OrderByDescending(orderBySelector);
                else queryPredicate = queryPredicate.OrderBy(orderBySelector);
            }

            if (skip > 0)
            {
                queryPredicate = query.Skip(skip);
            }

            if (take > 0)
            {
                queryPredicate = query.Take(take);
            }

            return queryPredicate;
        }
    }
}
