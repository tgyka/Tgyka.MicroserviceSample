using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggregate;

namespace Tgyka.Microservice.OrderService.Infrastructure
{
    public class OrderServiceDbContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
