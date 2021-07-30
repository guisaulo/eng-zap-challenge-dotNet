using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.RealEstates.Application.Mappers
{
    public class GeoLocationProfile : Profile
    {
        public GeoLocationProfile()
        {
            RealEstateMap();
            RealEstateDtoMap();
        }

        private void RealEstateMap()
        {
            CreateMap<GeoLocationDTO, GeoLocation>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(x => x.Location));
        }

        private void RealEstateDtoMap()
        {
            CreateMap<GeoLocation, GeoLocationDTO>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(x => x.Location));
        }
    }
}
