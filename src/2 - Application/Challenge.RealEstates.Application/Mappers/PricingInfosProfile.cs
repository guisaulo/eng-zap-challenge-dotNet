using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Domain.Entities;

namespace Challenge.RealEstates.Application.Mappers
{
    public class PricingInfosProfile : Profile
    {
        public PricingInfosProfile()
        {
            PricingInfosMap();
            PricingInfosDtoMap();
        }

        private void PricingInfosMap()
        {
            CreateMap<PricingInfosDTO, PricingInfos>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(x => long.Parse(x.Price)))
                .ForMember(dest => dest.RentalTotalPrice, opt => opt.MapFrom(x => long.Parse(x.RentalTotalPrice)))
                .ForMember(dest => dest.MonthlyCondoFee, opt => opt.MapFrom(x => long.Parse(x.MonthlyCondoFee)))
                .ForMember(dest => dest.YearlyIptu, opt => opt.MapFrom(x => long.Parse(x.YearlyIptu)));

        }

        private void PricingInfosDtoMap()
        {
            CreateMap<PricingInfos, PricingInfosDTO>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(x => x.Price.ToString()))
                .ForMember(dest => dest.RentalTotalPrice, opt => opt.MapFrom(x => x.RentalTotalPrice.ToString()))
                .ForMember(dest => dest.MonthlyCondoFee, opt => opt.MapFrom(x => x.MonthlyCondoFee.ToString()))
                .ForMember(dest => dest.YearlyIptu, opt => opt.MapFrom(x => x.YearlyIptu.ToString()));
        }
    }
}
