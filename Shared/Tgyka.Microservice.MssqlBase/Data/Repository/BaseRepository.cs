using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using System.Security.Principal;
using AutoMapper;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MssqlDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;

        public BaseRepository(MssqlDbContext dbContext, IUnitOfWork unitofWork, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public TEntity Get(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false)
        {
            return Query(predicate, includes, orderBySelector, isDescending).FirstOrDefault();
        }

        public IEnumerable<TEntity> List(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0)
        {
            return Query(predicate, includes, orderBySelector, isDescending, page, size).ToList();
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

        public TMapped GetWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false)
        {
            return _mapper.Map<TMapped>(Get(predicate,includes,orderBySelector,isDescending));
        }

        public List<TMapped> ListWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
    Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0)
        {
            return _mapper.Map<List<TMapped>>(List(predicate, includes, orderBySelector, isDescending,page,size));
        }

        public async Task<TMapped> SetWithCommit<TRequest,TMapped>(TRequest request, EntityState state)
        {
            var entity = _mapper.Map<TEntity>(request);
            var entityResponse = Set(entity, state);
            await _unitofWork.CommitAsync();
            return _mapper.Map<TMapped>(entityResponse);
        }

        public async Task<List<TMapped>> SetWithCommit<TRequest, TMapped>(List<TRequest> requests, EntityState state)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(requests);
            var entitiesResponse = Set(entities, state);
            await _unitofWork.CommitAsync();
            return _mapper.Map<List<TMapped>>(entitiesResponse);
        }

        private IEnumerable<TEntity> Query(Func<TEntity, bool> predicate, List<Expression<Func<TEntity, object>>> includes,
            Func<TEntity, object> orderBySelector, bool isDescending, int page = 0, int size = 0)
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

            if (page > 0)
            {
                queryPredicate = query.Skip((page - 1) * size) ;
            }

            if (size > 0)
            {
                queryPredicate = query.Take(size);
            }

            return queryPredicate;
        }
    }
}
