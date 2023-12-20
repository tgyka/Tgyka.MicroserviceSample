using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.DDDBase;

namespace Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate
{
    public class Order : Entity , IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;

        public Order(string buyerId, OrderStatus status, Address address)
        {
            BuyerId = buyerId;
            _orderItems = new List<OrderItem>();
            Status = status;
            Address = address;
        }

        public Order()
        {

        }

        public string BuyerId { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems; 
        public OrderStatus Status { get; private set; }
        public virtual Address Address { get; private set; }

        public void AddOrderItem(int productId , string productName , double price , string imageUrl)
        {
            _orderItems.Add(new OrderItem(productId, productName, imageUrl, price));
        }

        public void SetOrderStatus(OrderStatus status)
        {
            Status = status;
        }

        public void SetCreated(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetModified(string modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
