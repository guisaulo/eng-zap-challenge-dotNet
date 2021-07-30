using AutoMapper;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return new LoadRealEstatesCommandResponse
            {
                DateAddRangeCreate = DateTime.Now.ToString(),
                CountInvalidInputIds = domainResult.InvalidInputIds.ToList().Count(),
                CountZapIllegibleIds = domainResult.ZapIllegibleIds.ToList().Count(),
                CountVivaRealIneligibleIds = domainResult.VivaRealIneligibleIds.ToList().Count(),
                InvalidInputIds = domainResult.InvalidInputIds,
                ZapIllegibleIds = domainResult.ZapIllegibleIds,
                VivaRealIneligibleIds = domainResult.VivaRealIneligibleIds
            };
        }

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