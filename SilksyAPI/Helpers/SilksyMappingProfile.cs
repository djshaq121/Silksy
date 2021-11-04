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

            CreateMap<Cart, CartDto>().ForMember(dest => dest.CartItems, opt => opt.MapFrom(s => s.CartItems)).ReverseMap().ForAllOtherMembers(x => x.Ignore());

            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
