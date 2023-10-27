using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPanelService
    {
        Task<ApiResponseDto<ProductPanelResponseDto>> CreateProduct(ProductPanelCreateRequestDto productRequest);
        Task<ApiResponseDto<ProductPanelResponseDto>> DeleteProduct(int productId);
        Task<ApiResponseDto<PaginationList<CategorySelectBoxResponseDto>>> ListCategoriesSelectBox();
        ApiResponseDto<PaginationList<ProductGridPanelResponseDto>> ListProductsGrid(int page, int size);
        Task<ApiResponseDto<ProductPanelResponseDto>> UpdateProduct(ProductPanelUpdateRequestDto productRequest);
    }
}
