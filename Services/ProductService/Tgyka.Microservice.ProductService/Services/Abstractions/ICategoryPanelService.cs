using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface ICategoryPanelService
    {
        Task<ApiResponse<CategoryPanelDto>> CreateCategory(CategoryPanelCreateDto categoryRequest);
        Task<ApiResponse<CategoryPanelDto>> DeleteCategory(int categoryId);
        ApiResponse<PaginationList<CategoryGridPanelDto>> ListCategorysGrid(int page, int size);
        Task<ApiResponse<CategoryPanelDto>> UpdateCategory(CategoryPanelUpdateDto categoryRequest);
    }
}
