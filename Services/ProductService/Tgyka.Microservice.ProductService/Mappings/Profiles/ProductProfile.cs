using AutoMapper;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Mappings.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {

            CreateMap<Product, ProductPanelCreateDto>().ReverseMap();
            CreateMap<Product, ProductPanelUpdateDto>().ReverseMap();
            CreateMap<Product, ProductGridPanelDto>().ReverseMap();
            CreateMap<Product, ProductPageDto>().ReverseMap();
            CreateMap<Product, ProductPanelDto>().ReverseMap();
        }
    }
}
