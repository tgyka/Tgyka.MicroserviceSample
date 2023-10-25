using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
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

        public ApiResponseDto<PaginationList<ProductGridPanelResponseDto>> ListProductsGrid(int page, int size)
        {
            var data = _productRepository.ListWithMapper<ProductGridPanelResponseDto>(page: page, size: size);
            return ApiResponseDto<PaginationList<ProductGridPanelResponseDto>>.Success(200, data);
        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> CreateProduct(ProductPanelCreateRequestDto productRequest)
        {
            var data = await _productRepository.SetWithCommit<ProductPanelCreateRequestDto, ProductPanelResponseDto>(productRequest, CommandState.Create);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);

        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> UpdateProduct(ProductPanelUpdateRequestDto productRequest)
        {
            var data = await _productRepository.SetWithCommit<ProductPanelUpdateRequestDto, ProductPanelResponseDto>(productRequest, CommandState.Update);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> DeleteProduct(int productId)
        {
            var entity = _productRepository.Get(r => r.Id == productId);
            var data = await _productRepository.SetWithCommit<Product, ProductPanelResponseDto>(entity, CommandState.SoftDelete);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<PaginationList<CategorySelectBoxResponse>>> ListCategoriesSelectBox()
        {
            var data = _categoryRepository.ListWithMapper<CategorySelectBoxResponse>();
            return ApiResponseDto<PaginationList<CategorySelectBoxResponse>>.Success(200, data);
        }
    }
}
