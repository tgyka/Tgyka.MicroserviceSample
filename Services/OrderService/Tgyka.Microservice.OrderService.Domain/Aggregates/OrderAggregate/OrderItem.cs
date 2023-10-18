using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Core;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggregate
{
    public class OrderItem: Entity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
    }
}
