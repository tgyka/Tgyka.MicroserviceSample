﻿using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.BasketService.Helpers;
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

        public BasketService(IOptions<RedisSettings> settings)
        {
            _settings = settings.Value;
            _database = RedisConnectionHelper.GetConnection(_settings).GetDatabase(_settings.Database);
        }

        public async Task<ApiResponse<BasketResponseDto>> GetBasket(string userId)
        {
            var data = await _database.StringGetAsync(userId);

            if (string.IsNullOrEmpty(data))
            {
                return ApiResponse<BasketResponseDto>.Error(404,"Basket is not found");
            }

            return ApiResponse<BasketResponseDto>.Success(200, JsonSerializer.Deserialize<BasketResponseDto>(data));
        }

        public async Task<ApiResponse<BasketResponseDto>> Upsert(BasketRequestDto request)
        {
            var result = await _database.StringSetAndGetAsync(request.UserId,JsonSerializer.Serialize(request));

            return ApiResponse<BasketResponseDto>.Success(200, JsonSerializer.Deserialize<BasketResponseDto>(result.ToString()));
        }

        public async Task<ApiResponse<bool>> Delete(string userId)
        {
            var status = await _database.KeyDeleteAsync(userId);

            return status ? ApiResponse<bool>.Success(204,status) : ApiResponse<bool>.Error(404, "Basket not found");
        }
    }
}
