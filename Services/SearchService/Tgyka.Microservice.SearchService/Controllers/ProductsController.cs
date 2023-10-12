using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.SearchService.Model.Dtos;
using Tgyka.Microservice.SearchService.Services.Abstractions;

namespace Tgyka.Microservice.SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : TgykaMicroserviceControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> GetProducts(string searchString,int page,int size,bool priceIsDescending = false)
        {
            return ApiActionResult<List<ProductResponseDto>>(await _productService.GetProducts(searchString, page, size, priceIsDescending));
        }
    }
}
