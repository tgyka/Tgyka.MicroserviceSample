using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.DDDBase;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate
{
    public class OrderItem : Entity
    {
        public OrderItem(int productId, string productName, string imageUrl, double price)
        {
            ProductId = productId;
            ProductName = productName;
            ImageUrl = imageUrl;
            Price = price;
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ImageUrl { get; private set; }
        public double Price { get; private set; }
        public int  OrderId { get; private set; }
    }
}
