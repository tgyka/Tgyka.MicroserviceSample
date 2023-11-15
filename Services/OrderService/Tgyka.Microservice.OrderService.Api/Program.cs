using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.Middlewares;
using Tgyka.Microservice.OrderService.Api;
using Tgyka.Microservice.OrderService.Application.Consumers;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(OrderDto));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
builder.Services.AddThisDbContext();
builder.Services.AddRepositories();


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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
