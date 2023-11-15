using AutoMapper;
using Tgyka.Microservice.Rabbitmq.Events;
using Tgyka.Microservice.SearchService.Model.Dtos;

namespace Tgyka.Microservice.SearchService.Mappings.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreatedEvent, ProductDto>().ReverseMap();
            CreateMap<ProductUpdatedEvent, ProductDto>().ReverseMap();
        }
    }
}
