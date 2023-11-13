using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using System.Security.Principal;
using AutoMapper;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MssqlDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseRepository(MssqlDbContext dbContext, IUnitOfWork unitofWork, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _unitOfWork = unitofWork;
            _mapper = mapper;
        }

        public TEntity Get(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false)
        {
            return Query(predicate, includes, orderBySelector, isDescending).FirstOrDefault();
        }

        public PaginationList<TEntity> List(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0)
        {
            var query = Query(predicate, includes, orderBySelector, isDescending, page, size);
            var data = query.ToList();
            var count = query.Count();
            return new PaginationList<TEntity>(data,count,page,size);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity Set(TEntity entity, CommandState state)
        {
            if(state == CommandState.SoftDelete)
            {
                entity.IsDeleted = true;
            }

            if(state == CommandState.Update)
            {
                var olderEntity = Get(r => r.Id == entity.Id);
                entity.CreatedBy = olderEntity.CreatedBy;
                entity.CreatedDate = olderEntity.CreatedDate;
            }

            _dbSet.Entry(entity).State = GetEntityStateFromCommandState(state);
            return entity;
        }

        public List<TEntity> Set(IEnumerable<TEntity> entitites, CommandState state)
        {
            foreach (var entry in entitites)
            {
                Set(entry, state);
            }
            return entitites.ToList();
        }

        public TMapped GetWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false)
        {
            return _mapper.Map<TMapped>(Get(predicate,includes,orderBySelector,isDescending));
        }

        public PaginationList<TMapped> ListWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0)
        {
            var response = List(predicate, includes, orderBySelector, isDescending, page, size);
            var mappedData = _mapper.Map<List<TMapped>>(response.DataList);
            return new PaginationList<TMapped>(mappedData,response.Count,response.Page,response.Size);
        }

        public async Task<TMapped> SetWithCommit<TRequest,TMapped>(TRequest request, CommandState state)
        {
            var entity = _mapper.Map<TEntity>(request);
            var entityResponse = Set(entity, state);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TMapped>(entityResponse);
        }

        public async Task<List<TMapped>> SetWithCommit<TRequest, TMapped>(List<TRequest> requests, CommandState state)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(requests);
            var entitiesResponse = Set(entities, state);
            await _dbContext.SaveChangesAsync();
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

        private EntityState GetEntityStateFromCommandState(CommandState commandState)
        {
            switch (commandState)
            {
                case CommandState.Create:
                    return EntityState.Added;
                case CommandState.Update:
                    return EntityState.Modified;
                case CommandState.SoftDelete:
                    return EntityState.Modified;
                case CommandState.HardDelete:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }

    public enum CommandState
    {
        Create,
        Update,
        SoftDelete,
        HardDelete
    }
}
