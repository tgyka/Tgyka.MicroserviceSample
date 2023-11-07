using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.SearchService.Model.Dtos;

namespace Tgyka.Microservice.SearchService.Services.Abstractions
{
    public interface IProductService
    {
        Task<ApiResponseDto<ProductDto>> CreateProduct(ProductDto request);
        Task<ApiResponseDto<bool>> DeleteProduct(int productId);
        Task<ApiResponseDto<List<ProductDto>>> GetProducts(string searchString, int page, int size, bool priceIsDescending);
        Task<ApiResponseDto<ProductDto>> UpdateProduct(ProductDto request);
    }
}
