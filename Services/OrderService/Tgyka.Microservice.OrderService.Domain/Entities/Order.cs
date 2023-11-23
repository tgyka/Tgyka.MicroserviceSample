using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Data.Entity;
using Tgyka.Microservice.OrderService.Domain.Enums;

namespace Tgyka.Microservice.OrderService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string BuyerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus Status { get; set; }
        public virtual Address Address { get; set; }
    }
}
