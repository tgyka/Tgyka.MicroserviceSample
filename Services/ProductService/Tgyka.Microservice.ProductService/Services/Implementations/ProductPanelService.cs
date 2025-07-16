using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.MssqlBase.Data.Enum;
using Tgyka.Microservice.MssqlBase.Data.Repository;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
using Tgyka.Microservice.ProductService.Model.Dtos.Product;
using Tgyka.Microservice.ProductService.Services.Abstractions;
using Tgyka.Microservice.Rabbitmq.Events;

namespace Tgyka.Microservice.ProductService.Services.Implementations
{
    public class ProductPanelService: IProductPanelService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISendEndpoint _sendEndpoint;

        public ProductPanelService(IProductRepository productRepository, ICategoryRepository categoryRepository, ISendEndpoint sendEndpoint)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _sendEndpoint = sendEndpoint;
        }

        public ApiResponse<PaginationModel<ProductGridPanelDto>> GetProductsGrid(int page, int size)
        {
            var data = _productRepository.GetAllMapped<ProductGridPanelDto>(page: page, size: size);
            return ApiResponse<PaginationModel<ProductGridPanelDto>>.Success(200, data);
        }

        public async Task<ApiResponse<PaginationModel<CategorySelectBoxDto>>> GetCategoriesSelectBox()
        {
            var data = _categoryRepository.GetAllMapped<CategorySelectBoxDto>();
            return ApiResponse<PaginationModel<CategorySelectBoxDto>>.Success(200, data);
        }

        public async Task<ApiResponse<ProductPanelDto>> CreateProduct(ProductPanelCreateDto productRequest)
        {
            var data = await _productRepository.SetAndCommit<ProductPanelCreateDto, ProductPanelDto>(productRequest, EntityCommandType.Create);

            var category = _categoryRepository.GetOne(r => r.Id == productRequest.CategoryId);

            if(category == null)
            {
                ApiResponse<ProductPanelDto>.Error(400,"Category is not found");
            }

            await _sendEndpoint.Send(new ProductCreatedEvent(data.Id,data.Name,data.Description,data.Price,data.Stock,data.CategoryId,category.Name));

            return ApiResponse<ProductPanelDto>.Success(201, data);
        }

        public async Task<ApiResponse<ProductPanelDto>> UpdateProduct(ProductPanelUpdateDto productRequest)
        {
            var data = await _productRepository.SetAndCommit<ProductPanelUpdateDto, ProductPanelDto>(productRequest, EntityCommandType.Update);

            var category = _categoryRepository.GetOne(r => r.Id == productRequest.CategoryId);

            if (category == null)
            {
                ApiResponse<ProductPanelDto>.Error(400, "Category is not found");
            }

            await _sendEndpoint.Send(new ProductUpdatedEvent(data.Id, data.Name, data.Description, data.Price, data.Stock, data.CategoryId,category.Name));

            return ApiResponse<ProductPanelDto>.Success(200, data);
        }

        public async Task<ApiResponse<ProductPanelDto>> DeleteProduct(int productId)
        {
            var entity = _productRepository.GetOne(r => r.Id == productId);
            var data = await _productRepository.SetAndCommit<Product, ProductPanelDto>(entity, EntityCommandType.SoftDelete);
            await _sendEndpoint.Send(new ProductDeletedEvent(productId));
            return ApiResponse<ProductPanelDto>.Success(200, data);
        }
    }
}
