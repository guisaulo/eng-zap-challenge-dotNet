using Challenge.RealEstates.Domain.Core.Interfaces.Repositories;
using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using System.Collections.Generic;
using System.Linq;
using Challenge.RealEstates.Domain.DomainResponse;

namespace Challenge.RealEstates.Domain.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IRealEstateValidationService _realEstateValidationService;
        private readonly IRealEstateRepository _realEstateRepository;
        public RealEstateService(IRealEstateValidationService realEstateValidationService, IRealEstateRepository realEstateRepository)
        {
            _realEstateValidationService = realEstateValidationService;
            _realEstateRepository = realEstateRepository;
        }

        public AddRangeResponse AddRange(IEnumerable<RealEstate> realEstates)
        {
            var domainResponse = new AddRangeResponse();

            if (realEstates == null || !realEstates.Any())
                return domainResponse;

            foreach (var realEstate in realEstates)
            {
                if (_realEstateValidationService.IsRealEstateInputValid(realEstate))
                {
                    domainResponse.Input.ValidIds.Add(realEstate.Id);
                    AddDataZap(realEstate, domainResponse);
                    AddDataViva(realEstate, domainResponse);
                }
                else
                    domainResponse.Input.InvalidIds.Add(realEstate.Id);
            }

            return domainResponse;
        }

        private void AddDataZap(RealEstate realEstate, AddRangeResponse domainResponse)
        {
            if (_realEstateValidationService.IsEligibleToZapPortal(realEstate))
            {
                domainResponse.Zap.ValidIds.Add(realEstate.Id);
                _realEstateRepository.AddZapRealEstate(realEstate);
            }
            else
                domainResponse.Zap.InvalidIds.Add(realEstate.Id);
        }

        private void AddDataViva(RealEstate realEstate, AddRangeResponse domainResponse)
        {
            if (_realEstateValidationService.IsEligibleToVivaRealPortal(realEstate))
            {
                domainResponse.VivaReal.ValidIds.Add(realEstate.Id);
                _realEstateRepository.AddVivaRealEstate(realEstate);
            }
            else
                domainResponse.VivaReal.InvalidIds.Add(realEstate.Id);
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filters filter)
        {
            return filter?.Source == null ? new PagedResponse<RealEstate>() : _realEstateRepository.GetAllPaged(pagedParams, filter);
        }
    }
}