using AutoMapper;
using Tgyka.Microservice.OrderService.Application.Models.Dtos.Address;
using Tgyka.Microservice.OrderService.Domain.Entities;

namespace Tgyka.Microservice.OrderService.Application.Mappings.Profiles
{
    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
