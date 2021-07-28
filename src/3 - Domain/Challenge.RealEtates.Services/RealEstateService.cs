using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using Challenge.RealEtates.Services.Properties;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Challenge.RealEtates.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly ILogger<RealEstateService> _logger;
        private readonly IValidator<RealEstate> _validator;
        private readonly IRealEstateRepository _realEstateRepository;
        public RealEstateService(ILogger<RealEstateService> logger, IValidator<RealEstate> validator, IRealEstateRepository realEstateRepository)
        {
            _logger = logger;
            _validator = validator;
            _realEstateRepository = realEstateRepository;
        }

        public void AddRange(IEnumerable<RealEstate> realEstates)
        {            
            foreach (var realEstate in realEstates)
            {
                if (ValidateRealEstate(realEstate))
                {
                    //AddDataZap(realEstate);
                    //AddDataViva(realEstate);
                }
            }
        }

        private void AddDataViva(RealEstate realEstate)
        {
            //ImplementaRegraDeNegócio
            _realEstateRepository.AddZapRealEstate(realEstate);
        }

        private void AddDataZap(RealEstate realEstate)
        {
            //ImplementaRegraDeNegócio
            _realEstateRepository.AddVivaRealEstate(realEstate);
        }

        private bool ValidateRealEstate(RealEstate realEstate)
        {
            var result = _validator.Validate(realEstate);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)                
                    _logger.LogError(string.Format(Resources.ValidationError, realEstate.Id, failure.PropertyName, failure.ErrorMessage));
                
                return false;
            }
            return true;
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter)
        {
            return _realEstateRepository.GetAllPaged(pagedParams, filter);
        }
    }
}