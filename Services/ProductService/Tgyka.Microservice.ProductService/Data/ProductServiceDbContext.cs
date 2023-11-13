using Microsoft.EntityFrameworkCore;
using System.Net;
using Tgyka.Microservice.MssqlBase.Data;
using Tgyka.Microservice.ProductService.Data.Configurationss;
using Tgyka.Microservice.ProductService.Data.Entities;

namespace Tgyka.Microservice.ProductService.Data
{
    public class ProductServiceDbContext: MssqlDbContext
    {
        public ProductServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
