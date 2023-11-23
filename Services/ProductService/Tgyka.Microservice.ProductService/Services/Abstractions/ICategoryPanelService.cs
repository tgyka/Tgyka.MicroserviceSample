using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface ICategoryPanelService
    {
        Task<ApiResponse<CategoryPanelDto>> CreateCategory(CategoryPanelCreateDto categoryRequest);
        Task<ApiResponse<CategoryPanelDto>> DeleteCategory(int categoryId);
        ApiResponse<PaginationModel<CategoryGridPanelDto>> GetCategorysGrid(int page, int size);
        Task<ApiResponse<CategoryPanelDto>> UpdateCategory(CategoryPanelUpdateDto categoryRequest);
    }
}
