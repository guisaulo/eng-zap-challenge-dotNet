using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Domain.Entities;

namespace Challenge.RealEstates.Application.Mappers
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            RealEstateMap();
            RealEstateDtoMap();
        }

        private void RealEstateMap()
        {
            CreateMap<AddressDTO, Address>()
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(x => x.GeoLocation));
        }

        private void RealEstateDtoMap()
        {
            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(x => x.GeoLocation));
        }
    }
}
