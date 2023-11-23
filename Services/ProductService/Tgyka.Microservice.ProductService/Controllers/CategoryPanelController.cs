using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;
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

        [HttpGet("getCategorysGrid")]
        public IActionResult GetCategorysGrid(int page, int size)
        {
            return ApiActionResult(_categoryPanelService.GetCategorysGrid(page,size));

        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory(CategoryPanelCreateDto categoryRequest)
        {
            return ApiActionResult(await _categoryPanelService.CreateCategory(categoryRequest));
        }

        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryPanelUpdateDto categoryRequest)
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
