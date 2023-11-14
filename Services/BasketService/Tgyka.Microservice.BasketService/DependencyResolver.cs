using Microsoft.OpenApi.Models;
using Tgyka.Microservice.BasketService.Services.Abstractions;
using Tgyka.Microservice.BasketService.Services.Implementations;
using Tgyka.Microservice.BasketService.Settings;

namespace Tgyka.Microservice.BasketService
{
    public static class DependencyResolver
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Service API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, Services.Implementations.BasketService>();
        }

        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
        }
    }
}
