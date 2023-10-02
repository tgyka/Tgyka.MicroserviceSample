using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPanelService
    {
        Task<ApiResponseDto<ProductPanelResponseDto>> CreateProduct(ProductPanelCreateRequestDto productRequest);
        Task<ApiResponseDto<ProductPanelResponseDto>> DeleteProduct(int productId);
        Task<ApiResponseDto<List<CategorySelectBoxResponse>>> ListCategoriesSelectBox();
        ApiResponseDto<ProductGridPanelResponseDto> ListProductsGrid(int page, int size);
        Task<ApiResponseDto<ProductPanelResponseDto>> UpdateProduct(ProductPanelUpdateRequestDto productRequest);
    }
}
