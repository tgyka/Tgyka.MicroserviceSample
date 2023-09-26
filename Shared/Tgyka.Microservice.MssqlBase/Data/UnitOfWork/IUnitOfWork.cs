namespace Tgyka.Microservice.MssqlBase.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
