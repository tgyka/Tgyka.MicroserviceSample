using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
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

        [HttpGet("listProductsGrid")]
        public IActionResult ListProductsGrid(int page, int size)
        {
            return ApiActionResult(_productPanelService.ListProductsGrid(page, size));
        }

        [HttpGet("listCategoriesSelectBox")]
        public async Task<IActionResult> ListCategoriesSelectBox()
        {
            return ApiActionResult(await _productPanelService.ListCategoriesSelectBox());
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductPanelCreateRequestDto productRequest)
        {
            return ApiActionResult(await _productPanelService.CreateProduct(productRequest));
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductPanelUpdateRequestDto productRequest)
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
