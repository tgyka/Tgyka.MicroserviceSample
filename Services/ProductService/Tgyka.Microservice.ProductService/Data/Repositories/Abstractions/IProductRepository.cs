using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.ProductService.Data.Entities;

namespace Tgyka.Microservice.ProductService.Data.Repositories.Abstractions
{
    public interface IProductRepository: IBaseRepository<Product>
    {
    }
}
