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
        }
    }
}
