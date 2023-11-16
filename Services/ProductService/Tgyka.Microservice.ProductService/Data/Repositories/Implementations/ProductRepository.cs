using AutoMapper;
using Tgyka.Microservice.Base.Model.Token;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;

namespace Tgyka.Microservice.ProductService.Data.Repositories.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MssqlDbContext dbContext, IUnitOfWork unitofWork, IMapper mapper, TokenUser tokenUser) : base(dbContext, unitofWork, mapper, tokenUser)
        {
        }
    }
}
