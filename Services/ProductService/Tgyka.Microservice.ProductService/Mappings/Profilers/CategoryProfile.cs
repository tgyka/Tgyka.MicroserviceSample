using AutoMapper;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Requests;
using Tgyka.Microservice.ProductService.Model.Dtos.Category.Responses;
using Tgyka.Microservice.ProductService.Model.Dtos.Product.Requests;

namespace Tgyka.Microservice.ProductService.Mappings.Profilers
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryPanelCreateRequestDto>().ReverseMap();
            CreateMap<Category, CategoryPanelUpdateRequestDto>().ReverseMap();
            CreateMap<Category, CategoryGridPanelResponseDto>().ReverseMap();
            CreateMap<Category, CategoryPageResponseDto>().ReverseMap();
            CreateMap<Category, CategoryPanelResponseDto>().ReverseMap();
            CreateMap<Category, CategorySelectBoxResponseDto>().ReverseMap();
        }
    }
}
