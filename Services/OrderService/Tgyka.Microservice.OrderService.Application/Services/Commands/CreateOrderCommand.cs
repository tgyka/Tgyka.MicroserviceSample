using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Address;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Domain.Entities;

namespace Tgyka.Microservice.OrderService.Application.Services.Commands
{
    public class CreateOrderCommand: IRequest<ApiResponse<OrderDto>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
        public OrderStatus Status => OrderStatus.Created;
        public DateTime CreatedDate => DateTime.UtcNow;

    }
}
