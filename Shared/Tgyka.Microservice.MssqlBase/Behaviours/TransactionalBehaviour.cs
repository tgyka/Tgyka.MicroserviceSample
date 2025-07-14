using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tgyka.Microservice.MssqlBase.Behaviours
{
    public class TransactionalBehaviour<TRequest, TResponse> : ITransactionalBehaviour<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly DbContext _dbContext;

        public TransactionalBehaviour(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                return await next();
            }

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
