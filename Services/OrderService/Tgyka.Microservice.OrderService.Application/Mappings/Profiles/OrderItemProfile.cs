using AutoMapper;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.OrderItem;
using Tgyka.Microservice.OrderService.Domain.Aggregates.OrderAggreegate;

namespace Tgyka.Microservice.OrderService.Application.Mappings.Profiles
{
    public class OrderItemProfile: Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<OrderItemCreateDto, OrderItem>().ReverseMap();

        }
    }
}
