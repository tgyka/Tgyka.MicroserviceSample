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

        public ApiResponse<PaginationList<CategoryGridPanelDto>> ListCategorysGrid(int page, int size)
        {
            var data = _categoryRepository.ListWithMapper<CategoryGridPanelDto>(page: page, size: size);
            return ApiResponse<PaginationList<CategoryGridPanelDto>>.Success(200, data);
        }

        public async Task<ApiResponse<CategoryPanelDto>> CreateCategory(CategoryPanelCreateDto categoryRequest)
        {
            var data = await _categoryRepository.SetWithCommit<CategoryPanelCreateDto, CategoryPanelDto>(categoryRequest, CommandState.Create);
            return ApiResponse<CategoryPanelDto>.Success(201, data);

        }

        public async Task<ApiResponse<CategoryPanelDto>> UpdateCategory(CategoryPanelUpdateDto categoryRequest)
        {
            var data = await _categoryRepository.SetWithCommit<CategoryPanelUpdateDto, CategoryPanelDto>(categoryRequest, CommandState.Update);
            return ApiResponse<CategoryPanelDto>.Success(200, data);
        }

        public async Task<ApiResponse<CategoryPanelDto>> DeleteCategory(int categoryId)
        {
            var entity = _categoryRepository.Get(r => r.Id == categoryId);
            var data = await _categoryRepository.SetWithCommit<Category, CategoryPanelDto>(entity, CommandState.SoftDelete);
            return ApiResponse<CategoryPanelDto>.Success(200, data);
        }
    }
}
