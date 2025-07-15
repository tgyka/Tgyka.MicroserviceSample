using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base;
using Tgyka.Microservice.Base.Middlewares;
using Tgyka.Microservice.OrderService.Api;
using Tgyka.Microservice.OrderService.Application.Consumers;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Infrastructure;
using FluentValidation;
using Tgyka.Microservice.OrderService.Application.Services.Commands.CreateOrder;
using Tgyka.Microservice.OrderService.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();

builder.Services.AddAuthenticationAndBindTokenUser(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductStockNotReservedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitMqUri"));

        cfg.ReceiveEndpoint("product-stock-not-reserved", e =>
        {
            e.ConfigureConsumer<ProductStockNotReservedEventConsumer>(context);
        });

        cfg.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(10)));

    });
});

builder.Services.AddDbContext<OrderServiceDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), configure =>
    {
        configure.MigrationsAssembly("Tgyka.Microservice.OrderService.Infrastructure");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
