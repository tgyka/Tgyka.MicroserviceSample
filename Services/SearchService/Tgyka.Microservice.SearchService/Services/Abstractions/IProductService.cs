using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.SearchService.Model.Dtos;

namespace Tgyka.Microservice.SearchService.Services.Abstractions
{
    public interface IProductService
    {
        Task<ApiResponse<ProductDto>> CreateProduct(ProductDto request);
        Task<ApiResponse<bool>> DeleteProduct(int productId);
        Task<ApiResponse<List<ProductDto>>> GetProducts(string searchString, int page, int size, bool priceIsDescending);
        Task<ApiResponse<ProductDto>> UpdateProduct(ProductDto request);
    }
}
