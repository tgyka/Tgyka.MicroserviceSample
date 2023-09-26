using Tgyka.Microservice.MssqlBase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tgyka.Microservice.MssqlBase.Data.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int skip = 0, int take = 0);
        IEnumerable<TEntity> List(Func<TEntity, bool> predicate = null, List<Expression<Func<TEntity, object>>> includes = null,
            Func<TEntity, object> orderBySelector = null, bool isDescending = false, int skip = 0, int take = 0);
        TEntity Set(TEntity entity, EntityState state);
        List<TEntity> Set(IEnumerable<TEntity> entitites, EntityState state);
    }
}
