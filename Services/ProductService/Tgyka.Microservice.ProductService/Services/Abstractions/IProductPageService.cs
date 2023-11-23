using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
using Tgyka.Microservice.ProductService.Model.Dtos.Product;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPageService
    {
        ApiResponse<PaginationModel<CategoryPageDto>> GetCategories();
        ApiResponse<ProductPageDto> GetProductById(int productId);
        ApiResponse<PaginationModel<ProductPageDto>> GetProductsByCategoryId(int categoryId, int page, int size);
    }
}
