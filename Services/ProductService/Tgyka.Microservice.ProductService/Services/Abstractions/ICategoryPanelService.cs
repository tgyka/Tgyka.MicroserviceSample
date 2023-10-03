using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;

namespace Tgyka.Microservice.ProductService.Services.Abstractions
{
    public interface ICategoryPanelService
    {
        Task<ApiResponseDto<CategoryPanelResponseDto>> CreateCategory(CategoryPanelCreateRequestDto categoryRequest);
        Task<ApiResponseDto<CategoryPanelResponseDto>> DeleteCategory(int categoryId);
        ApiResponseDto<CategoryGridPanelResponseDto> ListCategorysGrid(int page, int size);
        Task<ApiResponseDto<CategoryPanelResponseDto>> UpdateCategory(CategoryPanelUpdateRequestDto categoryRequest);
    }
}
