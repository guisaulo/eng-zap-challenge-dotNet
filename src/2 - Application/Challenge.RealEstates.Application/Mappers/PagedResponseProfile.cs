using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.PagedParam;

namespace Challenge.RealEstates.Application.Mappers
{
    public class PagedResponseProfile : Profile
    {
        public PagedResponseProfile()
        {
            PagedResponseMap();
            PagedResponseDtoMap();
        }

        private void PagedResponseMap()
        {
            CreateMap<PagedParamsDto<RealEstateDTO>, PagedResponse<RealEstate>>()
                .ForMember(dest => dest.Listings, opt => opt.MapFrom(x => x.Listings));
        }
        
        private void PagedResponseDtoMap()
        {
            CreateMap<PagedResponse<RealEstate>, PagedParamsDto<RealEstateDTO>>()
                .ForMember(dest => dest.Listings, opt => opt.MapFrom(x => x.Listings));
        }
    }
}