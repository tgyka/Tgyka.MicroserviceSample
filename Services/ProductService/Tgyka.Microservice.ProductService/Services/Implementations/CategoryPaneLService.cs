using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Data.Repositories.Implementations;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;
using Tgyka.Microservice.ProductService.Services.Abstractions;

namespace Tgyka.Microservice.ProductService.Services.Implementations
{
    public class CategoryPanelService: ICategoryPanelService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryPanelService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ApiResponse<PaginationList<CategoryGridPanelResponseDto>> ListCategorysGrid(int page, int size)
        {
            var data = _categoryRepository.ListWithMapper<CategoryGridPanelResponseDto>(page: page, size: size);
            return ApiResponse<PaginationList<CategoryGridPanelResponseDto>>.Success(200, data);
        }

        public async Task<ApiResponse<CategoryPanelResponseDto>> CreateCategory(CategoryPanelCreateRequestDto categoryRequest)
        {
            var data = await _categoryRepository.SetWithCommit<CategoryPanelCreateRequestDto, CategoryPanelResponseDto>(categoryRequest, CommandState.Create);
            return ApiResponse<CategoryPanelResponseDto>.Success(200, data);

        }

        public async Task<ApiResponse<CategoryPanelResponseDto>> UpdateCategory(CategoryPanelUpdateRequestDto categoryRequest)
        {
            var data = await _categoryRepository.SetWithCommit<CategoryPanelUpdateRequestDto, CategoryPanelResponseDto>(categoryRequest, CommandState.Update);
            return ApiResponse<CategoryPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponse<CategoryPanelResponseDto>> DeleteCategory(int categoryId)
        {
            var entity = _categoryRepository.Get(r => r.Id == categoryId);
            var data = await _categoryRepository.SetWithCommit<Category, CategoryPanelResponseDto>(entity, CommandState.SoftDelete);
            return ApiResponse<CategoryPanelResponseDto>.Success(200, data);
        }
    }
}
