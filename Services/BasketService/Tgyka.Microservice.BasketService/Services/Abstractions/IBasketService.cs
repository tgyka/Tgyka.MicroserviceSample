using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.BasketService.Models.Dtos;

namespace Tgyka.Microservice.BasketService.Services.Abstractions
{
    public interface IBasketService
    {
        Task<ApiResponse<bool>> Delete(int userId);
        Task<ApiResponse<BasketResponseDto>> GetBasket(int userId);
        Task<ApiResponse<BasketResponseDto>> Upsert(BasketRequestDto request);
    }
}
