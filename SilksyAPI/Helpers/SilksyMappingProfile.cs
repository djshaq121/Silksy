using AutoMapper;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Helpers
{
    public class SilksyMappingProfile : Profile
    {
        public SilksyMappingProfile()
        {
            CreateMap<RegisterDto, StoreUser>();

            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(s => s.CartItems))
                .ReverseMap()
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CartItem, CartItemDto>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(p => p.Brand.Name))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(p => p.ProductCategories.Select(pc => pc.Category.Name)));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => new Brand { Name = src.Brand })); // Do reason we need this mapping, is because I represent the brand as a string and not a BrandDto inside the productDto class

            CreateMap<Brand, BrandDto>()
                .ReverseMap();

            //.ForMember(dest => dest.Brand, opt => opt.MapFrom(b => b.Name))

            //CreateMap<Brand, string>()
            //    .ConvertUsing(s => s.Name ?? string.Empty);
            
            // Stack Overflow second anwser
            //CreateMap<string, Brand>()
            //.ConvertUsing(source => new Brand { Name = source });

            CreateMap<Category, CategoryDto>()
                .ReverseMap();
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(c => c.Name))
        }
    }
}
