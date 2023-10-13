using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.SearchService.Model.Dtos;

namespace Tgyka.Microservice.SearchService.Services.Abstractions
{
    public interface IProductService
    {
        Task<ApiResponseDto<ProductResponseDto>> CreateProduct(ProductResponseDto request);
        Task<ApiResponseDto<bool>> DeleteProduct(int productId);
        Task<ApiResponseDto<List<ProductResponseDto>>> GetProducts(string searchString, int page, int size, bool priceIsDescending);
    }
}
