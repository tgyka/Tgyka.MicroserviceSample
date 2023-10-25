using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPageService
    {
        ApiResponseDto<PaginationList<CategoryPageResponseDto>> GetCategories();
        ApiResponseDto<ProductPageResponseDto> GetProductById(int productId);
        ApiResponseDto<PaginationList<ProductPageResponseDto>> GetProductsByCategoryId(int categoryId, int page, int size);
    }
}
