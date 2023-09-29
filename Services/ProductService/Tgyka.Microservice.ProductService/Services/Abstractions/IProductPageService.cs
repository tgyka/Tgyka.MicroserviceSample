using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPageService
    {
        List<CategoryPageResponseDto> GetCategories();
        ProductPageResponseDto GetProductById(int productId);
        List<ProductPageResponseDto> GetProductsByCategoryId(int categoryId, int page, int size);
    }
}
