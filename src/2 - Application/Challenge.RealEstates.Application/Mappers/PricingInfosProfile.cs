using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEtates.Domain.Entities;

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
            CreateMap<PricingInfosDTO, PricingInfos>();
        }

        private void PricingInfosDtoMap()
        {
            CreateMap<PricingInfos, PricingInfosDTO>();
        }
    }
}
