using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.OrderService.Domain.Core;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggregate
{
    public class Order: Entity , IAggregateRoot
    {
        public int BuyerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
