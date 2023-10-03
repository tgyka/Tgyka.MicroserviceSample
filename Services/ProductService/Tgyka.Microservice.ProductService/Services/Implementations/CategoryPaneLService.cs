using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.Model.ApiResponse;
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

        public ApiResponseDto<CategoryGridPanelResponseDto> ListCategorysGrid(int page, int size)
        {
            var data = _categoryRepository.ListWithMapper<CategoryGridPanelResponseDto>(page: page, size: size);
            return ApiResponseDto<CategoryGridPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<CategoryPanelResponseDto>> CreateCategory(CategoryPanelCreateRequestDto categoryRequest)
        {
            var data = await _categoryRepository.SetWithCommit<CategoryPanelCreateRequestDto, CategoryPanelResponseDto>(categoryRequest, EntityState.Added);
            return ApiResponseDto<CategoryPanelResponseDto>.Success(200, data);

        }

        public async Task<ApiResponseDto<CategoryPanelResponseDto>> UpdateCategory(CategoryPanelUpdateRequestDto categoryRequest)
        {
            var data = await _categoryRepository.SetWithCommit<CategoryPanelUpdateRequestDto, CategoryPanelResponseDto>(categoryRequest, EntityState.Modified);
            return ApiResponseDto<CategoryPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<CategoryPanelResponseDto>> DeleteCategory(int categoryId)
        {
            var entity = _categoryRepository.Get(r => r.Id == categoryId);
            var data = await _categoryRepository.SetWithCommit<Category, CategoryPanelResponseDto>(entity, EntityState.Deleted);
            return ApiResponseDto<CategoryPanelResponseDto>.Success(200, data);
        }
    }
}
