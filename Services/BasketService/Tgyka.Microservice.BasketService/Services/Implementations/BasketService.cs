using StackExchange.Redis;
using System.Text.Json;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.BasketService.Models.Dtos;
using Tgyka.Microservice.BasketService.Services.Abstractions;
using Tgyka.Microservice.BasketService.Settings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tgyka.Microservice.BasketService.Services.Implementations
{
    public class BasketService: IBasketService
    {
        private readonly RedisSettings _settings;
        private readonly IDatabase _database;

        public BasketService(RedisSettings settings)
        {
            _settings = settings;
            _database = ConnectionMultiplexer.Connect($"({settings.Host}:{settings.Port}").GetDatabase(settings.Database);
        }

        public async Task<ApiResponse<BasketResponseDto>> GetBasket(int userId)
        {
            var data = await _database.StringGetAsync(userId.ToString());

            if (string.IsNullOrEmpty(data))
            {
                return ApiResponse<BasketResponseDto>.Error(404,"Basket is not found");
            }

            return ApiResponse<BasketResponseDto>.Success(200, JsonSerializer.Deserialize<BasketResponseDto>(data));
        }

        public async Task<ApiResponse<BasketResponseDto>> Upsert(BasketRequestDto request)
        {
            var result = await _database.StringSetAndGetAsync(request.UserId.ToString(),JsonSerializer.Serialize(request));

            return ApiResponse<BasketResponseDto>.Success(200, JsonSerializer.Deserialize<BasketResponseDto>(result));
        }

        public async Task<ApiResponse<bool>> Delete(int userId)
        {
            var status = await _database.KeyDeleteAsync(userId.ToString());

            return status ? ApiResponse<bool>.Success(204,status) : ApiResponse<bool>.Error(404, "Basket not found");
        }
    }
}
