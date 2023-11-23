using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.BasketService.Models.Dtos;
using Tgyka.Microservice.BasketService.Services.Abstractions;

namespace Tgyka.Microservice.BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketsController : TgykaMicroserviceControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            return ApiActionResult(await _basketService.GetBasket(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(BasketUpsertDto request)
        {
            return ApiActionResult(await _basketService.Upsert(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string userId)
        {
            return ApiActionResult(await _basketService.Delete(userId));
        }

    }
}
