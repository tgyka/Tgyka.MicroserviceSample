using Microsoft.OpenApi.Models;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.OrderService.Infrastructure;
using Tgyka.Microservice.OrderService.Domain.Repositories;
using Tgyka.Microservice.OrderService.Infrastructure.Repositories;

namespace Tgyka.Microservice.OrderService.Api
{
    public static class DependencyResolver
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tgyka Microservice Order Api V1", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}
                    }
                });
            });
        }

        public static void AddThisDbContext(this IServiceCollection services) => services.AddScoped<MssqlDbContext, OrderServiceDbContext>();

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
