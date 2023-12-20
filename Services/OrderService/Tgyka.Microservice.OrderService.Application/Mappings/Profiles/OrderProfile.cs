using AutoMapper;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Order;
using Tgyka.Microservice.OrderService.Application.Services.Commands;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate;

namespace Tgyka.Microservice.OrderService.Application.Mappings.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderCommand, Order>().ReverseMap();
        }
    }
}
