using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Address;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate;

namespace Tgyka.Microservice.OrderService.Application.Services.Commands
{
    public class CreateOrderCommand: IRequest<ApiResponse<OrderDto>>
    {
        public List<OrderItemCreateDto> OrderItems { get; set; }
        public AddressCreateDto Address { get; set; }
        public OrderStatus Status => OrderStatus.Created;
        public DateTime CreatedDate => DateTime.UtcNow;
    }
}
