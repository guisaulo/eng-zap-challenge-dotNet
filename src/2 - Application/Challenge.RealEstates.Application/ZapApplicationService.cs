using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Params;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System;
using System.Collections.Generic;

namespace Challenge.RealEstates.Application
{
    public class ZapApplicationService : IZapApplicationService
    {
        private readonly IZapService _zapService;
        private readonly IMapper _mapper;
        public ZapApplicationService(IZapService zapService, IMapper mapper)
        {
            _zapService = zapService;
            _mapper = mapper;
        }
        public PagedResponseDTO<RealEstateDTO> GetAllPaged(QueryParamsDTO param)
        {
            var response = _zapService.GetAllPaged(GetPagedParams(param), GetFilter(param));
            return _mapper.Map<PagedResponseDTO<RealEstateDTO>>(response);
        }

        private static PagedParams GetPagedParams(QueryParamsDTO queryParam) =>
            new()
            {
                PageNumber = queryParam.PageNumber,
                PageSize = queryParam.PageSize
            };

        private static Filter GetFilter(QueryParamsDTO param) =>
            new()
            {
                UsableAreas = param.UsableAreas,
                ParkingSpaces = param.ParkingSpaces,
                City = param.City,
                Bathrooms = param.Bathrooms,
                Bedrooms = param.Bedrooms,
                BusinessType = param.BusinessType,
                Price = param.Price
            };

        public bool LoadSource()
        {
            return _zapService.LoadSource();
        }
    }
}
