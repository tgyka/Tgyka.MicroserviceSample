using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.BasketService.Models.Dtos;

namespace Tgyka.Microservice.BasketService.Services.Abstractions
{
    public interface IBasketService
    {
        Task<ApiResponse<bool>> Delete(string userId);
        Task<ApiResponse<BasketDto>> GetBasket(string userId);
        Task<ApiResponse<BasketDto>> Upsert(BasketUpsertDto request);
    }
}
