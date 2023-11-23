using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using System.Security.Principal;
using AutoMapper;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.Base.Model.Token;
using Tgyka.Microservice.MssqlBase.Data.Enum;
using System.Drawing;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MssqlDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly TokenUser _tokenUser;

        public BaseRepository(MssqlDbContext dbContext, IUnitOfWork unitofWork, IMapper mapper, TokenUser tokenUser)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _unitOfWork = unitofWork;
            _mapper = mapper;
            _tokenUser = tokenUser;
        }

        public TEntity GetOne(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Expression<Func<TEntity, object>> orderBySelector = null, bool isDescending = false)
        {
            return QueryCore(predicate, includes, orderBySelector, isDescending).FirstOrDefault();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Expression<Func<TEntity, object>> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0)
        {
            return QueryCore(predicate, includes, orderBySelector, isDescending, page, size).ToList();
        }

        public TMapped GetOneMapped<TMapped>(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Expression<Func<TEntity, object>> orderBySelector = null, bool isDescending = false)
        {
            return _mapper.Map<TMapped>(GetOne(predicate, includes, orderBySelector, isDescending));
        }

        public PaginationModel<TMapped> GetAllMapped<TMapped>(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Expression<Func<TEntity, object>> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0)
        {
            var query = QueryCore(predicate, includes, orderBySelector, isDescending, page, size);
            var count = query.Count();
            var mappedData = _mapper.Map<List<TMapped>>(query.ToList());
            return new PaginationModel<TMapped>(mappedData, count, page, size);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Expression<Func<TEntity, object>> orderBySelector = null, bool isDescending = false)
        {
            return QueryCore(predicate, includes, orderBySelector, isDescending, 0, 0).Count();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsNoTracking();
        }

        public void SetEntityState(TEntity entity, EntityCommandType type,string? userId = null)
        {
            var tokenUserId = userId ?? _tokenUser.Id;

            if (type == EntityCommandType.SoftDelete)
            {
                entity.IsDeleted = true;
            }

            if(type == EntityCommandType.Update)
            {
                var olderEntity = GetOne(r => r.Id == entity.Id);
                entity.CreatedBy = olderEntity.CreatedBy;
                entity.CreatedDate = olderEntity.CreatedDate;
                entity.ModifiedBy = tokenUserId;
            }

            if(type == EntityCommandType.Create)
            {
                entity.CreatedBy = tokenUserId;
            }

            _dbSet.Entry(entity).State = GetEntityStateFromEntityCommandType(type);
        }

        public void SetEntityState(IEnumerable<TEntity> entitites, EntityCommandType type, string? userId = null)
        {
            foreach (var entry in entitites)
            {
                SetEntityState(entry, type, userId);
            }
        }

        public async Task<TMapped> SetAndCommit<TRequest,TMapped>(TRequest request, EntityCommandType type,string? userId = null)
        {
            var entity = _mapper.Map<TEntity>(request);
            SetEntityState(entity, type, userId);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TMapped>(entity);
        }

        public async Task<List<TMapped>> SetAndCommit<TRequest, TMapped>(List<TRequest> requests, EntityCommandType type, string? userId = null)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(requests);
            SetEntityState(entities, type, userId);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<List<TMapped>>(entities);
        }

        private IEnumerable<TEntity> QueryCore(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes,
            Expression<Func<TEntity, object>> orderBySelector, bool isDescending, int page = 0, int size = 0)
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

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBySelector != null)
            {
                if (isDescending) 
                    query = query.OrderByDescending(orderBySelector);
                else 
                    query = query.OrderBy(orderBySelector);
            }

            if (page > 0)
            {
                query = query.Skip((page - 1) * size) ;
            }

            if (size > 0)
            {
                query = query.Take(size);
            }

            return query;
        }

        private EntityState GetEntityStateFromEntityCommandType(EntityCommandType entityCommandType)
        {
            switch (entityCommandType)
            {
                case EntityCommandType.Create:
                    return EntityState.Added;
                case EntityCommandType.Update:
                    return EntityState.Modified;
                case EntityCommandType.SoftDelete:
                    return EntityState.Modified;
                case EntityCommandType.HardDelete:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
