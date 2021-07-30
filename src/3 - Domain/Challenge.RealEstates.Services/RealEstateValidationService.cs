using Challenge.RealEstates.Services.Properties;
using Challenge.RealEstates.Core.Interfaces.Services;
using Challenge.RealEstates.Domain.Constants;
using Challenge.RealEstates.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Challenge.RealEstates.Services
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
                "RENT" => IsEligibleRentToZapPortal(realEstate),
                _ => false
            };
        }

        public bool IsEligibleToVivaRealPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.BusinessType switch
            {
                "SALE" => IsEligibleSaleToVivaRealPortal(realEstate),
                "RENT" => IsEligibleRentToVivaRealPortal(realEstate),
                _ => false
            };
        }

        private static bool IsEligibleSaleToZapPortal(RealEstate realEstate)
        {
            var minValueSaleZap = IsWithinTheZapGroupBoundingBox(realEstate)
                ? BusinessConstants.MinValueSaleZap * ((100 - BusinessConstants.PercentageBoundingBoxMinValueSaleZap) / 100)
                : BusinessConstants.MinValueSaleZap;

            return realEstate.UsableAreas is > 0 and > BusinessConstants.MinValueUsableAreaSaleZap 
                   && realEstate.PricingInfos.Price >= minValueSaleZap;
        }

        private static bool IsEligibleRentToZapPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.RentalTotalPrice > 0 
                   && realEstate.PricingInfos.RentalTotalPrice >= BusinessConstants.MinValueRentZap;
        }

        private static bool IsEligibleSaleToVivaRealPortal(RealEstate realEstate)
        {
            return realEstate.PricingInfos.Price <= BusinessConstants.MaxValueSaleVivaReal;
        }

        private static bool IsEligibleRentToVivaRealPortal(RealEstate realEstate)
        {
            var maxValueRentVivaReal = IsWithinTheZapGroupBoundingBox(realEstate)
                ? BusinessConstants.MaxValueRentVivaReal * ((100 + BusinessConstants.PercentageBoundingBoxMaxValueVivaReal) / 100)
                : BusinessConstants.MaxValueRentVivaReal;

            return realEstate.PricingInfos.MonthlyCondoFee > 0
                   && realEstate.PricingInfos.MonthlyCondoFee < realEstate.PricingInfos.RentalTotalPrice * (BusinessConstants.PercentageRentPriceVivaReal / 100)
                   && realEstate.PricingInfos.Price >= maxValueRentVivaReal;
        }

        private static bool IsWithinTheZapGroupBoundingBox(RealEstate realEstate)
        {
            return realEstate.Address.GeoLocation.Location.Lat is <= BoundingBoxZapGroupConstants.MaxLat and >= BoundingBoxZapGroupConstants.MinLat
                   && realEstate.Address.GeoLocation.Location.Lon is <= BoundingBoxZapGroupConstants.MaxLon and >= BoundingBoxZapGroupConstants.MinLon;
        }
    }
}