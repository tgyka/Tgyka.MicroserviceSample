﻿using AutoMapper;
using Tgyka.Microservice.ProductService.Data.Entities;
using Tgyka.Microservice.ProductService.Model.Dtos.Category;

namespace Tgyka.Microservice.ProductService.Mappings.Profiles
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryPanelCreateDto>().ReverseMap();
            CreateMap<Category, CategoryPanelUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryGridPanelDto>().ReverseMap();
            CreateMap<Category, CategoryPageDto>().ReverseMap();
            CreateMap<Category, CategoryPanelDto>().ReverseMap();
            CreateMap<Category, CategorySelectBoxDto>().ReverseMap();
        }
    }
}
