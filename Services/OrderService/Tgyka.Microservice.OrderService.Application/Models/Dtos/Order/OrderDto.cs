using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Model.Dtos;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Domain.Entities;

namespace Tgyka.Microservice.OrderService.Application.Models.Dtos.Order
{
    public class OrderDto : GetDto
    {
        public int BuyerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
    }
}
