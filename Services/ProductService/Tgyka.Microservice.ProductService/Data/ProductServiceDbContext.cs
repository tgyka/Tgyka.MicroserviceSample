using Microsoft.EntityFrameworkCore;
using System.Net;
using Tgyka.Microservice.ProductService.Data.Entities;

namespace Tgyka.Microservice.ProductService.Data
{
    public class ProductServiceDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
