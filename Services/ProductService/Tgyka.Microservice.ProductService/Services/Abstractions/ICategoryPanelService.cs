using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface ICategoryPanelService
    {
        Task<ApiResponse<CategoryPanelResponseDto>> CreateCategory(CategoryPanelCreateRequestDto categoryRequest);
        Task<ApiResponse<CategoryPanelResponseDto>> DeleteCategory(int categoryId);
        ApiResponse<PaginationList<CategoryGridPanelResponseDto>> ListCategorysGrid(int page, int size);
        Task<ApiResponse<CategoryPanelResponseDto>> UpdateCategory(CategoryPanelUpdateRequestDto categoryRequest);
    }
}
