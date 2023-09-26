namespace Tgyka.Microservice.MssqlBase.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly MssqlDbContext _dbContext;

        public UnitOfWork(MssqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
