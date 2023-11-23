
using System.Drawing;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
using Tgyka.Microservice.ProductService.Model.Dtos.Product;
using Tgyka.Microservice.ProductService.Services.Abstractions;

namespace Tgyka.Microservice.ProductService.Services.Implementations
{
    public class ProductPageService: IProductPageService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductPageService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public ApiResponse<PaginationModel<CategoryPageDto>> GetCategories()
        {
            var data = _categoryRepository.GetAllMapped<CategoryPageDto>();
            return ApiResponse<PaginationModel<CategoryPageDto>>.Success(200, data);
        }

        public ApiResponse<PaginationModel<ProductPageDto>> GetProductsByCategoryId(int categoryId,int page , int size)
        {
            var data = _productRepository.GetAllMapped<ProductPageDto>(r => r.CategoryId == categoryId, null, r => r.CreatedDate, true, page, size);
            return ApiResponse<PaginationModel<ProductPageDto>>.Success(200, data);
        }

        public ApiResponse<ProductPageDto> GetProductById(int productId)
        {
            var data = _productRepository.GetOneMapped<ProductPageDto>(r => r.Id == productId);
            return ApiResponse<ProductPageDto>.Success(200, data);
        }
    }
}
