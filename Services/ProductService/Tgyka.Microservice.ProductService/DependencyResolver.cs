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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tgyka Microservice Product Api V1", Version = "v1" });

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

        public static void AddThisDbContext(this IServiceCollection services) => services.AddScoped<MssqlDbContext, ProductServiceDbContext>();

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
