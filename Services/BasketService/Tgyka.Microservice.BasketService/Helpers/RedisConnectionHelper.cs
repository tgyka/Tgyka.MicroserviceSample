using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Runtime;
using Tgyka.Microservice.BasketService.Settings;

namespace Tgyka.Microservice.BasketService.Helpers
{
    public static class RedisConnectionHelper
    {
        private static ConnectionMultiplexer _connection;

        public static ConnectionMultiplexer GetConnection(RedisSettings settings)
        {
            if (_connection == null)
            {
                var options = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    EndPoints = { settings.Host, settings.Port }
                };
                _connection = ConnectionMultiplexer.Connect(options);
            }
            return _connection;
        }
    }
}
