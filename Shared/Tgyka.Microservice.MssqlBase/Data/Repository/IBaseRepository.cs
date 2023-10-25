using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<TEntity, object> orderBySelector = null, bool isDescending = false);
        IQueryable<TEntity> GetQuery();
        TMapped GetWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<TEntity, object> orderBySelector = null, bool isDescending = false);
        PaginationList<TEntity> List(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0);
        PaginationList<TMapped> ListWithMapper<TMapped>(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, Func<TEntity, object> orderBySelector = null, bool isDescending = false, int page = 0, int size = 0);
        TEntity Set(TEntity entity, CommandState state);
        List<TEntity> Set(IEnumerable<TEntity> entitites, CommandState state);
        Task<TMapped> SetWithCommit<TRequest, TMapped>(TRequest request, CommandState state);
        Task<List<TMapped>> SetWithCommit<TRequest, TMapped>(List<TRequest> requests, CommandState state);
    }
}
