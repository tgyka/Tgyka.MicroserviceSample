using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.ProductService.Model.Dtos.Product;
using Tgyka.Microservice.ProductService.Services.Abstractions;

namespace Tgyka.Microservice.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductPanelController : TgykaMicroserviceControllerBase
    {
        private IProductPanelService _productPanelService;

        public ProductPanelController(IProductPanelService productPanelService)
        {
            _productPanelService = productPanelService;
        }

        [HttpGet("getProductsGrid")]
        public IActionResult GetProductsGrid(int page, int size)
        {
            return ApiActionResult(_productPanelService.GetProductsGrid(page, size));
        }

        [HttpGet("getCategoriesSelectBox")]
        public async Task<IActionResult> GetCategoriesSelectBox()
        {
            return ApiActionResult(await _productPanelService.GetCategoriesSelectBox());
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductPanelCreateDto productRequest)
        {
            return ApiActionResult(await _productPanelService.CreateProduct(productRequest));
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductPanelUpdateDto productRequest)
        {
            return ApiActionResult(await _productPanelService.UpdateProduct(productRequest));
        }

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            return ApiActionResult(await _productPanelService.DeleteProduct(productId));
        }
    }
}
