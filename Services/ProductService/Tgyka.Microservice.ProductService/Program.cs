using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.M�ddlewares;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.ProductService;
using Tgyka.Microservice.ProductService.Consumers;
using Tgyka.Microservice.ProductService.Data;
using Tgyka.Microservice.Rabbitmq.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ProductServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddThisDbContext();
builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://identityserver.com";
                    options.Audience = "api1"; 
                });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductStockUpdatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        RabbitmqSettings rabbitmqSettings = new();
        builder.Configuration.GetSection("Rabbitmq").Bind(rabbitmqSettings);

        cfg.Host(rabbitmqSettings.Uri, "/", host =>
        {
            host.Username(rabbitmqSettings.Username);
            host.Password(rabbitmqSettings.Password);
        });

        cfg.ReceiveEndpoint("product-stock-updated", e =>
        {
            e.ConfigureConsumer<ProductStockUpdatedEventConsumer>(context);
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

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
