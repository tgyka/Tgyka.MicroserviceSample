using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tgyka.Microservice.ProductService;
using Tgyka.Microservice.ProductService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ProductServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mssql")));

builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://identityserver.com";
            options.Audience = "api1"; 
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
