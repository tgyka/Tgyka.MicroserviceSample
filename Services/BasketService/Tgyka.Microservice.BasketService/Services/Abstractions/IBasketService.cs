using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.BasketService.Models.Dtos;

namespace Tgyka.Microservice.BasketService.Services.Abstractions
{
    public interface IBasketService
    {
        Task<ApiResponseDto<bool>> Delete(int userId);
        Task<ApiResponseDto<BasketResponseDto>> GetBasket(int userId);
        Task<ApiResponseDto<BasketResponseDto>> Upsert(BasketRequestDto request);
    }
}
