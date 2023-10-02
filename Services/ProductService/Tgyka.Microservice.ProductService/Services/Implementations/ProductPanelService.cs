using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;
using Tgyka.Microservice.ProductService.Services.Abstractions;

namespace Tgyka.Microservice.ProductService.Services.Implementations
{
    public class ProductPanelService: IProductPanelService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductPanelService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ApiResponseDto<ProductGridPanelResponseDto> ListProductsGrid(int page, int size)
        {
            var data = _productRepository.ListWithMapper<ProductGridPanelResponseDto>(page: page, size: size);
            return ApiResponseDto<ProductGridPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> CreateProduct(ProductPanelCreateRequestDto productRequest)
        {
            var data = await _productRepository.SetWithCommit<ProductPanelCreateRequestDto, ProductPanelResponseDto>(productRequest, EntityState.Added);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);

        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> UpdateProduct(ProductPanelUpdateRequestDto productRequest)
        {
            var data = await _productRepository.SetWithCommit<ProductPanelUpdateRequestDto, ProductPanelResponseDto>(productRequest, EntityState.Modified);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> DeleteProduct(int productId)
        {
            var entity = _productRepository.Get(r => r.Id == productId);
            var data = await _productRepository.SetWithCommit<Product, ProductPanelResponseDto>(entity, EntityState.Deleted);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<List<CategorySelectBoxResponse>>> ListCategoriesSelectBox()
        {
            var data = _categoryRepository.ListWithMapper<CategorySelectBoxResponse>();
            return ApiResponseDto<List<CategorySelectBoxResponse>>.Success(200, data);
        }
    }
}
