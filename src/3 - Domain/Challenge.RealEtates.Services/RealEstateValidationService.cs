using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Constants;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Services.Properties;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Challenge.RealEtates.Services
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
                    _logger.LogError(string.Format(Resources.ValidationError, realEstate.Id, failure.PropertyName, failure.ErrorMessage));

                return false;
            }
            return true;
        }

        public bool IsEligibleToZapPortal(RealEstate realEtate)
        {
            return realEtate.PricingInfos.BusinessType switch
            {
                "SALE" => IsEligibleSaleToZapPortal(realEtate),
                "RENT" => IsEligibleRentToZapPortal(realEtate),
                _ => false
            };
        }

        public bool IsEligibleToVivaRealPortal(RealEstate realEtate)
        {
            return realEtate.PricingInfos.BusinessType switch
            {
                "SALE" => IsEligibleSaleToVivaRealPortal(realEtate),
                "RENT" => IsEligibleRentToVivaRealPortal(realEtate),
                _ => false
            };
        }

        private static bool IsEligibleSaleToZapPortal(RealEstate realEtate)
        {
            var minValueSaleZap = IsWithinTheZapGroupBoundingBox(realEtate)
                ? BusinessConstants.MinValueSaleZap * ((100 - BusinessConstants.PercentageBoundingBoxMinValueSaleZap) / 100)
                : BusinessConstants.MinValueSaleZap;

            return realEtate.UsableAreas is > 0 and > BusinessConstants.MinValueUsableAreaSaleZap 
                   && long.Parse(realEtate.PricingInfos.Price) >= minValueSaleZap;
        }

        private static bool IsEligibleRentToZapPortal(RealEstate realEtate)
        {
            return long.Parse(realEtate.PricingInfos.RentalTotalPrice) > 0 
                   && long.Parse(realEtate.PricingInfos.RentalTotalPrice) >= BusinessConstants.MinValueRentZap;
        }

        private static bool IsEligibleSaleToVivaRealPortal(RealEstate realEtate)
        {
            return long.Parse(realEtate.PricingInfos.Price) <= BusinessConstants.MaxValueSaleVivaReal;
        }

        private static bool IsEligibleRentToVivaRealPortal(RealEstate realEtate)
        {
            var maxValueRentVivaReal = IsWithinTheZapGroupBoundingBox(realEtate)
                ? BusinessConstants.MaxValueRentVivaReal * ((100 + BusinessConstants.PercentageBoundingBoxMaxValueVivaReal) / 100)
                : BusinessConstants.MaxValueRentVivaReal;

            return long.Parse(realEtate.PricingInfos.MonthlyCondoFee) > 0
                && long.Parse(realEtate.PricingInfos.MonthlyCondoFee) < long.Parse(realEtate.PricingInfos.RentalTotalPrice) * (BusinessConstants.PercentageRentPriceVivaReal / 100)
                && long.Parse(realEtate.PricingInfos.Price) >= maxValueRentVivaReal;
        }

        private static bool IsWithinTheZapGroupBoundingBox(RealEstate realEtate)
        {
            return realEtate.Address.GeoLocation.Location.Lat is <= BoundingBoxZapGroupConstants.MaxLat and >= BoundingBoxZapGroupConstants.MinLat
                   && realEtate.Address.GeoLocation.Location.Lon is <= BoundingBoxZapGroupConstants.MaxLon and >= BoundingBoxZapGroupConstants.MinLon;
        }
    }
}