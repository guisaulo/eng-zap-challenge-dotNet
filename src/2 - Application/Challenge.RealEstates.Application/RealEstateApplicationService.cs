using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Params;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System;
using System.Collections.Generic;

namespace Challenge.RealEstates.Application
{
    public class RealEstateApplicationService : IRealEstateApplicationService
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IMapper _mapper;
        public RealEstateApplicationService(IRealEstateService realEstateService, IMapper mapper)
        {
            _realEstateService = realEstateService;
            _mapper = mapper;
        }

        public void AddRange(IEnumerable<RealEstate> realEstates)
        {
            _realEstateService.AddRange(realEstates);
        }

        public PagedResponseDTO<RealEstateDTO> GetAllPaged(QueryParamsDTO param)
        {
            var response = _realEstateService.GetAllPaged(GetPagedParams(param), GetFilter(param));
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
                Source = param.Source,
                UsableAreas = param.UsableAreas,
                ParkingSpaces = param.ParkingSpaces,
                City = param.City,
                Bathrooms = param.Bathrooms,
                Bedrooms = param.Bedrooms,
                BusinessType = param.BusinessType,
                Price = param.Price
            };

    }
}