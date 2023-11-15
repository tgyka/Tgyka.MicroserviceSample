using MassTransit;
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
using Tgyka.Microservice.Rabbitmq.Events;

namespace Tgyka.Microservice.ProductService.Services.Implementations
{
    public class ProductPanelService: IProductPanelService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductPanelService(IProductRepository productRepository, ICategoryRepository categoryRepository, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _publishEndpoint = publishEndpoint;
        }

        public ApiResponse<PaginationList<ProductGridPanelDto>> ListProductsGrid(int page, int size)
        {
            var data = _productRepository.ListWithMapper<ProductGridPanelDto>(page: page, size: size);
            return ApiResponse<PaginationList<ProductGridPanelDto>>.Success(200, data);
        }

        public async Task<ApiResponse<PaginationList<CategorySelectBoxDto>>> ListCategoriesSelectBox()
        {
            var data = _categoryRepository.ListWithMapper<CategorySelectBoxDto>();
            return ApiResponse<PaginationList<CategorySelectBoxDto>>.Success(200, data);
        }

        public async Task<ApiResponse<ProductPanelDto>> CreateProduct(ProductPanelCreateDto productRequest)
        {
            var data = await _productRepository.SetWithCommit<ProductPanelCreateDto, ProductPanelDto>(productRequest, CommandState.Create);

            var category = _categoryRepository.Get(r => r.Id == productRequest.CategoryId);

            if(category == null)
            {
                ApiResponse<ProductPanelDto>.Error(400,"Category is not found");
            }

            _publishEndpoint.Publish(new ProductCreatedEvent(data.Id,data.Name,data.Description,data.Price,data.Stock,data.CategoryId,category.Name));

            return ApiResponse<ProductPanelDto>.Success(200, data);
        }

        public async Task<ApiResponse<ProductPanelDto>> UpdateProduct(ProductPanelUpdateDto productRequest)
        {
            var data = await _productRepository.SetWithCommit<ProductPanelUpdateDto, ProductPanelDto>(productRequest, CommandState.Update);

            var category = _categoryRepository.Get(r => r.Id == productRequest.CategoryId);

            if (category == null)
            {
                ApiResponse<ProductPanelDto>.Error(400, "Category is not found");
            }

            _publishEndpoint.Publish(new ProductUpdatedEvent(data.Id, data.Name, data.Description, data.Price, data.Stock, data.CategoryId,category.Name));

            return ApiResponse<ProductPanelDto>.Success(200, data);
        }

        public async Task<ApiResponse<ProductPanelDto>> DeleteProduct(int productId)
        {
            var entity = _productRepository.Get(r => r.Id == productId);
            var data = await _productRepository.SetWithCommit<Product, ProductPanelDto>(entity, CommandState.SoftDelete);
            _publishEndpoint.Publish(new ProductDeletedEvent(productId));
            return ApiResponse<ProductPanelDto>.Success(200, data);
        }
    }
}
