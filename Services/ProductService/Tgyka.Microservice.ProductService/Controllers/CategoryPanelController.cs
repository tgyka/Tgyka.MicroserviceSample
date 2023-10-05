using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Services.Abstractions;

namespace Tgyka.Microservice.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryPanelController : TgykaMicroserviceControllerBase
    {
        private ICategoryPanelService _categoryPanelService;

        public CategoryPanelController(ICategoryPanelService categoryPanelService)
        {
            _categoryPanelService = categoryPanelService;
        }

        [HttpGet("listCategorysGrid")]
        public IActionResult ListCategorysGrid(int page, int size)
        {
            return ApiActionResult(_categoryPanelService.ListCategorysGrid(page,size));

        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory(CategoryPanelCreateRequestDto categoryRequest)
        {
            return ApiActionResult(await _categoryPanelService.CreateCategory(categoryRequest));
        }

        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryPanelUpdateRequestDto categoryRequest)
        {
            return ApiActionResult(await _categoryPanelService.UpdateCategory(categoryRequest));
        }

        [HttpDelete("deleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            return ApiActionResult(await _categoryPanelService.DeleteCategory(categoryId));
        }
    }
}
