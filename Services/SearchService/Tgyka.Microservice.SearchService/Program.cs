using MassTransit;
using Microsoft.Extensions.Configuration;
using Tgyka.Microservice.Base.Middlewares;
using Tgyka.Microservice.SearchService.Consumers;
using Tgyka.Microservice.SearchService.Services.Abstractions;
using Tgyka.Microservice.SearchService.Services.Implementations;
using Tgyka.Microservice.SearchService.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductCreatedEventConsumer>();
    x.AddConsumer<ProductUpdatedEventConsumer>();
    x.AddConsumer<ProductDeletedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitMqUri"));

        cfg.ReceiveEndpoint("product-created", e =>
        {
            e.ConfigureConsumer<ProductCreatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("product-updated", e =>
        {
            e.ConfigureConsumer<ProductUpdatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("product-deleted", e =>
        {
            e.ConfigureConsumer<ProductDeletedEventConsumer>(context);
        });

        cfg.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(10)));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
