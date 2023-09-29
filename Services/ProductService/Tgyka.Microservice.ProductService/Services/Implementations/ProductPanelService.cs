using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Services.Implementations
{
    public class ProductPanelService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductPanelService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public List<ProductGridPanelResponseDto> GetProductsGrid(int page, int size)
        {
            return _productRepository.ListWithMapper<ProductGridPanelResponseDto>(page: page,size: size);
        }

        public void CreateProduct(ProductPanelCreateRequestDto product)
        {
             
        }
    }
}
