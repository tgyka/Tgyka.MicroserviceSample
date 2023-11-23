using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Data.Enum;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Data.Repositories.Implementations;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
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

        public ApiResponse<PaginationModel<CategoryGridPanelDto>> GetCategorysGrid(int page, int size)
        {
            var data = _categoryRepository.GetAllMapped<CategoryGridPanelDto>(page: page, size: size);
            return ApiResponse<PaginationModel<CategoryGridPanelDto>>.Success(200, data);
        }

        public async Task<ApiResponse<CategoryPanelDto>> CreateCategory(CategoryPanelCreateDto categoryRequest)
        {
            var data = await _categoryRepository.SetAndCommit<CategoryPanelCreateDto, CategoryPanelDto>(categoryRequest, EntityCommandType.Create);
            return ApiResponse<CategoryPanelDto>.Success(201, data);

        }

        public async Task<ApiResponse<CategoryPanelDto>> UpdateCategory(CategoryPanelUpdateDto categoryRequest)
        {
            var data = await _categoryRepository.SetAndCommit<CategoryPanelUpdateDto, CategoryPanelDto>(categoryRequest, EntityCommandType.Update);
            return ApiResponse<CategoryPanelDto>.Success(200, data);
        }

        public async Task<ApiResponse<CategoryPanelDto>> DeleteCategory(int categoryId)
        {
            var entity = _categoryRepository.GetOne(r => r.Id == categoryId);
            var data = await _categoryRepository.SetAndCommit<Category, CategoryPanelDto>(entity, EntityCommandType.SoftDelete);
            return ApiResponse<CategoryPanelDto>.Success(200, data);
        }
    }
}
