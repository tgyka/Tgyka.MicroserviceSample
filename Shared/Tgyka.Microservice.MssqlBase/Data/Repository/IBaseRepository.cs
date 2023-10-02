using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false);
        TMapped GetWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<TEntity, object> orderBySelector = null, bool isDescending = false);
        IEnumerable<TEntity> List(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0);
        List<TMapped> ListWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0);
        TEntity Set(TEntity entity, EntityState state);
        List<TEntity> Set(IEnumerable<TEntity> entitites, EntityState state);
        Task<TMapped> SetWithCommit<TRequest, TMapped>(TRequest request, EntityState state);
        Task<List<TMapped>> SetWithCommit<TRequest, TMapped>(List<TRequest> requests, EntityState state);
    }
}
