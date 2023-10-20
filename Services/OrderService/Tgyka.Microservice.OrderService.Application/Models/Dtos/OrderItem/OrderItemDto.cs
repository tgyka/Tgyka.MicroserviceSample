using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.MssqlBase.Model.Dtos;

namespace Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem
{
    public class OrderItemDto: GetDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
    }
}
