using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Domain.Entities;

namespace Challenge.RealEstates.Application.Mappers
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            LocationMap();
            LocationDtoMap();
        }

        private void LocationMap()
        {
            CreateMap<LocationDTO, Location>();
        }

        private void LocationDtoMap()
        {
            CreateMap<Location, LocationDTO>();
        }
    }
}