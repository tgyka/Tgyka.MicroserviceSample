
using System.Drawing;
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

        public List<CategoryPageResponseDto> GetCategories()
        {
            return _categoryRepository.ListWithMapper<CategoryPageResponseDto>();
        }

        public List<ProductPageResponseDto> GetProductsByCategoryId(int categoryId,int page , int size)
        {
            return _productRepository.ListWithMapper<ProductPageResponseDto>(r => r.CategoryId == categoryId,null,r => r.CreatedDate,true,page,size);
        }

        public ProductPageResponseDto GetProductById(int productId)
        {
            return _productRepository.GetWithMapper<ProductPageResponseDto>(r => r.Id == productId);
        }
    }
}
