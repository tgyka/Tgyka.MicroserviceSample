using AutoMapper;
using Tgyka.Microservice.Rabbitmq.Events;
using Tgyka.Microservice.SearchService.Model.Dtos;

namespace Tgyka.Microservice.SearchService.Mappings.Profilers
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductUpdatedEvent, ProductDto>().ReverseMap();
        }
    }
}
