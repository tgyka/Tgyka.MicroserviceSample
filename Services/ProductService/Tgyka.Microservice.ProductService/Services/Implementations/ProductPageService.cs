
using System.Drawing;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;
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

        public ApiResponse<PaginationList<CategoryPageResponseDto>> GetCategories()
        {
            var data = _categoryRepository.ListWithMapper<CategoryPageResponseDto>();
            return ApiResponse<PaginationList<CategoryPageResponseDto>>.Success(200, data);
        }

        public ApiResponse<PaginationList<ProductPageResponseDto>> GetProductsByCategoryId(int categoryId,int page , int size)
        {
            var data = _productRepository.ListWithMapper<ProductPageResponseDto>(r => r.CategoryId == categoryId, null, r => r.CreatedDate, true, page, size);
            return ApiResponse<PaginationList<ProductPageResponseDto>>.Success(200, data);
        }

        public ApiResponse<ProductPageResponseDto> GetProductById(int productId)
        {
            var data = _productRepository.GetWithMapper<ProductPageResponseDto>(r => r.Id == productId);
            return ApiResponse<ProductPageResponseDto>.Success(200, data);
        }
    }
}
