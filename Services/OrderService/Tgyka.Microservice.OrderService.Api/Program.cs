using MassTransit;
using Tgyka.Microservice.OrderService.Application.Consumers;
using Tgyka.Microservice.Rabbitmq.Events;
using Tgyka.Microservice.Rabbitmq.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductStockNotReservedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        RabbitmqSettings rabbitmqSettings = new();
        builder.Configuration.GetSection("Rabbitmq").Bind(rabbitmqSettings);

        cfg.Host(rabbitmqSettings.Uri, "/", host =>
        {
            host.Username(rabbitmqSettings.Username);
            host.Password(rabbitmqSettings.Password);
        });

        cfg.ReceiveEndpoint("product-stock-not-reserveds", e =>
        {
            e.ConfigureConsumer<ProductStockNotReservedEventConsumer>(context);
        });

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
