using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Challenge.RealEstates.Application.Command;

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

        public LoadRealEstatesCommandResponse AddRange(IEnumerable<RealEstate> realEstates)
        {
            var domainResult =_realEstateService.AddRange(realEstates);
            return GetLoadRealEstatesCommandResponseFromDomainResult(domainResult);
        }

        private static LoadRealEstatesCommandResponse GetLoadRealEstatesCommandResponseFromDomainResult(
            Domain.DomainResponse.AddRangeResponse domainResult) =>
            new()
            {
                LoadDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Input = new RealEstatesCommandResponse()
                {
                    TotalIds = domainResult.Input.ValidIds.Count + domainResult.Input.InvalidIds.Count,
                    ValidIds = domainResult.Input.ValidIds,
                    InvalidIds = domainResult.Input.InvalidIds,
                },
                Zap = new RealEstatesCommandResponse()
                {
                    TotalIds = domainResult.Zap.ValidIds.Count + domainResult.Zap.InvalidIds.Count,
                    ValidIds = domainResult.Zap.ValidIds,
                    InvalidIds = domainResult.Zap.InvalidIds,
                },
                VivaReal = new RealEstatesCommandResponse()
                {
                    TotalIds = domainResult.VivaReal.ValidIds.Count + domainResult.VivaReal.InvalidIds.Count,
                    ValidIds = domainResult.VivaReal.ValidIds,
                    InvalidIds = domainResult.VivaReal.InvalidIds,
                }
            };

        public PagedParamsDto<RealEstateDTO> GetAllPaged(string source, RealEstatesSearchDto search)
        {
            var response = _realEstateService.GetAllPaged(GetPagedParams(search), GetFilter(source, search));
            return _mapper.Map<PagedParamsDto<RealEstateDTO>>(response);
        }

        private static PagedParams GetPagedParams(RealEstatesSearchDto queryParam) =>
            new()
            {
                PageNumber = queryParam.PageNumber,
                PageSize = queryParam.PageSize
            };

        private static Filters GetFilter(string source, RealEstatesSearchDto param) =>
            new()
            {
                Source = source.ToLower(),
                City = param.City,
                BusinessType = param.BusinessType,
                Bathrooms = param.Bathrooms,
                Bedrooms = param.Bedrooms,
                ParkingSpaces = param.ParkingSpaces
            };

    }
}