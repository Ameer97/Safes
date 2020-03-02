using AutoMapper;
using Safes.Models.Db;
using Safes.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.Models.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Box, BoxCreateDto>();
            CreateMap<BoxCreateDto, Box>();

            CreateMap<OwnerCreateDto, Owner>();
            CreateMap<Owner, OwnerCreateDto>();

        }
    }
}
