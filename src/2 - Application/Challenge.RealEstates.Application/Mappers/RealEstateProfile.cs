using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Domain.Entities;

namespace Challenge.RealEstates.Application.Mappers
{
    public class RealEstateProfile : Profile
    {
        public RealEstateProfile()
        {
            RealEstateMap();
            RealEstateDtoMap();
        }

        private void RealEstateMap()
        {
            CreateMap<RealEstateDTO, RealEstate>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(x => x.Address))
                .ForMember(dest => dest.PricingInfos, opt => opt.MapFrom(x => x.PricingInfos));
        }

        private void RealEstateDtoMap()
        {
            CreateMap<RealEstate, RealEstateDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(x => x.Address))
                .ForMember(dest => dest.PricingInfos, opt => opt.MapFrom(x => x.PricingInfos));
        }
    }
}
