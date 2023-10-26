using AutoMapper;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Responses;

namespace Tgyka.Microservice.ProductService.Mappings.Profilers
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {

            CreateMap<Product, ProductPanelCreateRequestDto>().ReverseMap();
            CreateMap<Product, ProductPanelUpdateRequestDto>().ReverseMap();
            CreateMap<Product, ProductGridPanelResponseDto>().ReverseMap();
            CreateMap<Product, ProductPageResponseDto>().ReverseMap();
            CreateMap<Product, ProductPanelResponseDto>().ReverseMap();
        }
    }
}
