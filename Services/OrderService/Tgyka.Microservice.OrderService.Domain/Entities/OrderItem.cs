using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Data.Entity;

namespace Tgyka.Microservice.OrderService.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
