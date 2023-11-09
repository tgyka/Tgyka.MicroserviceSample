using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPanelService
    {
        Task<ApiResponse<ProductPanelResponseDto>> CreateProduct(ProductPanelCreateRequestDto productRequest);
        Task<ApiResponse<ProductPanelResponseDto>> DeleteProduct(int productId);
        Task<ApiResponse<PaginationList<CategorySelectBoxResponseDto>>> ListCategoriesSelectBox();
        ApiResponse<PaginationList<ProductGridPanelResponseDto>> ListProductsGrid(int page, int size);
        Task<ApiResponse<ProductPanelResponseDto>> UpdateProduct(ProductPanelUpdateRequestDto productRequest);
    }
}
