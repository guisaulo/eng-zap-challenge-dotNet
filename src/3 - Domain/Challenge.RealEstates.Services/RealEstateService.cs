using Challenge.RealEstates.Core.Interfaces.Repositories;
using Challenge.RealEstates.Core.Interfaces.Services;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using System.Collections.Generic;
using Challenge.RealEstates.Domain.DomainResponse;

namespace Challenge.RealEstates.Services
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
            var domainResponse = InicializeDomainResponse();

            foreach (var realEstate in realEstates)
            {
                if (_realEstateValidationService.IsRealEstateInputValid(realEstate))
                {
                    AddDataZap(realEstate, domainResponse);
                    AddDataViva(realEstate, domainResponse);
                }
                else
                    domainResponse.InvalidInputIds.Add(realEstate.Id);
            }

            return domainResponse;
        }

        private static AddRangeResponse InicializeDomainResponse()
        {
            return new AddRangeResponse
            {
                InvalidInputIds = new List<string>(),
                ZapIllegibleIds = new List<string>(),
                VivaRealIneligibleIds = new List<string>()
            };
        }

        private void AddDataZap(RealEstate realEstate, AddRangeResponse domainResponse)
        {
            if (_realEstateValidationService.IsEligibleToZapPortal(realEstate))
                _realEstateRepository.AddVivaRealEstate(realEstate);
            else
                domainResponse.ZapIllegibleIds.Add(realEstate.Id);
        }

        private void AddDataViva(RealEstate realEstate, AddRangeResponse domainResponse)
        {
            if (_realEstateValidationService.IsEligibleToVivaRealPortal(realEstate))
                _realEstateRepository.AddZapRealEstate(realEstate);
            else
                domainResponse.VivaRealIneligibleIds.Add(realEstate.Id);
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filters filter)
        {
            return _realEstateRepository.GetAllPaged(pagedParams, filter);
        }
    }
}