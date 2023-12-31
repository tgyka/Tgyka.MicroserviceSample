﻿using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.ProductService.Services.Abstractions;

namespace Tgyka.Microservice.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPageController : TgykaMicroserviceControllerBase
    {
        private IProductPageService _productPageService;

        public ProductPageController(IProductPageService productPageService)
        {
            _productPageService = productPageService;
        }

        [HttpGet("getCategories")]
        public IActionResult GetCategories()
        {
            return ApiActionResult(_productPageService.GetCategories());
        }

        [HttpGet("getProductsByCategoryId")]
        public IActionResult GetProductsByCategoryId(int categoryId, int page, int size)
        {
            return ApiActionResult(_productPageService.GetProductsByCategoryId(categoryId,page,size));
        }

        [HttpGet("getProductById")]
        public IActionResult GetProductById(int productId)
        {
            return ApiActionResult(_productPageService.GetProductById(productId));
        }

    }
}
