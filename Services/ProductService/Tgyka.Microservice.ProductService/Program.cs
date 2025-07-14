using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base;
using Tgyka.Microservice.Base.Middlewares;
using Tgyka.Microservice.ProductService;
using Tgyka.Microservice.ProductService.Consumers;
using Tgyka.Microservice.ProductService.Data;

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
builder.Services.AddAuthenticationAndBindTokenUser(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductStockUpdatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitMqUri"));

        cfg.ReceiveEndpoint("product-stock-updated", e =>
        {
            e.ConfigureConsumer<ProductStockUpdatedEventConsumer>(context);
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
