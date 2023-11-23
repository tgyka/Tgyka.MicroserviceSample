using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
using Tgyka.Microservice.ProductService.Model.Dtos.Product;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface IProductPanelService
    {
        Task<ApiResponse<ProductPanelDto>> CreateProduct(ProductPanelCreateDto productRequest);
        Task<ApiResponse<ProductPanelDto>> DeleteProduct(int productId);
        ApiResponse<PaginationModel<ProductGridPanelDto>> GetProductsGrid(int page, int size);
        Task<ApiResponse<PaginationModel<CategorySelectBoxDto>>> GetCategoriesSelectBox();
        Task<ApiResponse<ProductPanelDto>> UpdateProduct(ProductPanelUpdateDto productRequest);
    }
}
