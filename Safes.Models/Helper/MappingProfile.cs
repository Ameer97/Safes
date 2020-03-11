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
            //CreateMap<Box, BoxCreateDto>();
            CreateMap<BoxCreateDto, Box>();
            CreateMap<BoxUpdateReceivedDto, Box>();

            CreateMap<PersonCreateDto, Owner>();
            //CreateMap<Owner, PersonCreateDto>();

            CreateMap<PersonCreateDto, Meditor>();
            //CreateMap<Meditor, PersonCreateDto>();

            CreateMap<StaticBoxCreateDto, StaticBoxReuse>();

            CreateMap<StaticBoxReuse, StaticBoxReuseForViewDto>();
            CreateMap<StaticBox, StaticBoxAllReuseDto>();


            CreateMap<EventCreateDto, PlaceEvent>();


        }
    }
}
