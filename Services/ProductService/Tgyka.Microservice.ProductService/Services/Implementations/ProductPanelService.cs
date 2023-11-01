﻿using MassTransit;
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

            await _publishEndpoint.Publish(new ProductUpdateEvent(data.Id, data.Name, data.Description, data.Price, data.Stock, data.CategoryId));

            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<ProductPanelResponseDto>> DeleteProduct(int productId)
        {
            var entity = _productRepository.Get(r => r.Id == productId);
            var data = await _productRepository.SetWithCommit<Product, ProductPanelResponseDto>(entity, CommandState.SoftDelete);
            return ApiResponseDto<ProductPanelResponseDto>.Success(200, data);
        }

        public async Task<ApiResponseDto<PaginationList<CategorySelectBoxResponseDto>>> ListCategoriesSelectBox()
        {
            var data = _categoryRepository.ListWithMapper<CategorySelectBoxResponseDto>();
            return ApiResponseDto<PaginationList<CategorySelectBoxResponseDto>>.Success(200, data);
        }
    }
}
