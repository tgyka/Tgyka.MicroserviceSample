using MediatR;

namespace Tgyka.Microservice.MssqlBase.Behaviours
{
    public interface ITransactionalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
    }
}
