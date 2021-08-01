using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using Challenge.RealEstates.Domain.Constants;
using Challenge.RealEstates.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Challenge.RealEstates.Domain.Services.Properties;

namespace Challenge.RealEstates.Domain.Services
{
    public class RealEstateValidationService : IRealEstateValidationService
    {
        private readonly IValidator<RealEstate> _validator;
        private readonly ILogger<RealEstateService> _logger;
        public RealEstateValidationService(IValidator<RealEstate> validator, ILogger<RealEstateService> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public bool IsRealEstateInputValid(RealEstate realEstate)
        {
            var result = _validator.Validate(realEstate);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                    _logger.LogError(string.Format(Resource.ValidationError, realEstate.Id, failure.PropertyName, failure.ErrorMessage));

                return false;
            }
            return true;
        }

        public bool IsEligibleToZapPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.BusinessType switch
            {
                "SALE" => IsEligibleSaleToZapPortal(realEstate),
                "RENTAL" => IsEligibleRentalToZapPortal(realEstate),
                _ => false
            };
        }

        public bool IsEligibleToVivaRealPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.BusinessType switch
            {
                "SALE" => IsEligibleSaleToVivaRealPortal(realEstate),
                "RENTAL" => IsEligibleRentalToVivaRealPortal(realEstate),
                _ => false
            };
        }

        private static bool IsEligibleSaleToZapPortal(RealEstate realEstate)
        {
            var minValueSaleZap = IsWithinTheZapGroupBoundingBox(realEstate)
                ? BusinessConstants.MinValueSaleZap * ((100.0 - BusinessConstants.PercentageBoundingBoxMinValueSaleZap) / 100.0)
                : BusinessConstants.MinValueSaleZap;

            return realEstate.UsableAreas is > 0 
                   && (realEstate.PricingInfos.Price / realEstate.UsableAreas) > BusinessConstants.MinValueUsableAreaSaleZap 
                   && realEstate.PricingInfos.Price >= minValueSaleZap;
        }

        private static bool IsEligibleRentalToZapPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.RentalTotalPrice >= BusinessConstants.MinValueRentalZap;
        }

        private static bool IsEligibleSaleToVivaRealPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.Price <= BusinessConstants.MaxValueSaleVivaReal;
        }

        private static bool IsEligibleRentalToVivaRealPortal(RealEstate realEstate)
        {
            var maxValueRentalVivaReal = IsWithinTheZapGroupBoundingBox(realEstate)
                ? BusinessConstants.MaxValueRentalVivaReal * ((100.0 + BusinessConstants.PercentageBoundingBoxMaxValueVivaReal) / 100.0)
                : BusinessConstants.MaxValueRentalVivaReal;

            var rentPercentage = realEstate.PricingInfos.RentalTotalPrice * BusinessConstants.PercentageRentalPriceVivaReal / 100.0;


            return realEstate.PricingInfos.MonthlyCondoFee > 0
                   && realEstate.PricingInfos.MonthlyCondoFee < rentPercentage
                   && realEstate.PricingInfos.RentalTotalPrice <= maxValueRentalVivaReal;
        }

        private static bool IsWithinTheZapGroupBoundingBox(RealEstate realEstate)
        {
            var result = realEstate.Address.GeoLocation.Location.Lat is <= BoundingBoxZapGroupConstants.MaxLat and >= BoundingBoxZapGroupConstants.MinLat
                                 && realEstate.Address.GeoLocation.Location.Lon is <= BoundingBoxZapGroupConstants.MaxLon and >= BoundingBoxZapGroupConstants.MinLon;
            return result;
        }
    }
}