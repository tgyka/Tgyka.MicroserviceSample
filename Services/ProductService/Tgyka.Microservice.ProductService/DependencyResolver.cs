using Microsoft.OpenApi.Models;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.MssqlBase.Data.UnitOfWork;
using Tgyka.Microservice.ProductService.Data;
using Tgyka.Microservice.ProductService.Data.Repositories.Abstractions;
using Tgyka.Microservice.ProductService.Data.Repositories.Implementations;
using Tgyka.Microservice.ProductService.Services.Abstractions;
using Tgyka.Microservice.ProductService.Services.Implementations;

namespace Tgyka.Microservice.ProductService
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

        public static void AddThisDbContext(this IServiceCollection services) => services.AddTransient<MssqlDbContext, ProductServiceDbContext>();

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryPanelService, CategoryPanelService>();
            services.AddScoped<IProductPageService, ProductPageService>();
            services.AddScoped<IProductPanelService, ProductPanelService>();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
