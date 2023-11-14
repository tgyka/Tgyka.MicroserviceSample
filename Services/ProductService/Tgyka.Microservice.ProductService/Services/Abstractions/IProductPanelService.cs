using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPanelService
    {
        Task<ApiResponse<ProductPanelDto>> CreateProduct(ProductPanelCreateDto productRequest);
        Task<ApiResponse<ProductPanelDto>> DeleteProduct(int productId);
        Task<ApiResponse<PaginationList<CategorySelectBoxDto>>> ListCategoriesSelectBox();
        ApiResponse<PaginationList<ProductGridPanelDto>> ListProductsGrid(int page, int size);
        Task<ApiResponse<ProductPanelDto>> UpdateProduct(ProductPanelUpdateDto productRequest);
    }
}
