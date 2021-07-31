using Challenge.RealEstates.Domain.Core.Interfaces.Repositories;
using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Challenge.RealEstates.Domain.Entities;
using FluentValidation.Results;

namespace Challenge.RealEstates.Domain.Services.UnitTest.RealEstate.Fixture
{
    public class RealEstateFixture
    {
        public readonly RealEstateService Service;

        public RealEstateFixture()
        {
            Service = new RealEstateService(InitializeRealEstateValidationService(), InitializeRealEstateRepository());
        }

        private static IRealEstateValidationService InitializeRealEstateValidationService()
        {
            return new RealEstateValidationService(InitializeValidator(), InitializeLogger());
        }

        private static IRealEstateRepository InitializeRealEstateRepository()
        {
            var mock = new Mock<IRealEstateRepository>();
            return mock.Object;
        }

        private static IValidator<Entities.RealEstate> InitializeValidator()
        {
            var mock = new Mock<IValidator<Entities.RealEstate>>(MockBehavior.Strict);
            mock.Setup(a => a.Validate(It.IsAny<Entities.RealEstate>())).Returns(new ValidationResult());
            return mock.Object;
        }

        private static ILogger<RealEstateService> InitializeLogger()
        {
            var mock = new Mock<ILogger<RealEstateService>>();
            return mock.Object;
        }

        public static Entities.RealEstate GetValidRealEstateToSale(
            string id, 
            double lon = -46.716542, 
            double lat = -23.502555,
            int usableAreas = 4000,
            long price = 650000) =>
            new()
            {
                UsableAreas = usableAreas,
                ListingType = "USED",
                CreatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                ListingStatus = "ACTIVE",
                Id = id,
                ParkingSpaces = 2,
                UpdatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Owner = false,
                Images = new List<string>{"url"},
                Address = new Address()
                {
                    City = "Belo Horizonte",
                    Neighborhood = "Contagem",
                    GeoLocation = new GeoLocation()
                    {
                        Precision = "ROOFTOP",
                        Location = new Location()
                        {
                            Lon = lon,
                            Lat = lat
                        }
                    }
                },
                Bathrooms = 2,
                Bedrooms = 3,
                PricingInfos = new PricingInfos()
                {
                    YearlyIptu = 500,
                    Price = price,
                    RentalTotalPrice = 0,
                    BusinessType = "SALE",
                    MonthlyCondoFee = 500
                }
            };

        public static Entities.RealEstate GetValidRealEstateToRent(
            string id, 
            double lon = -46.716542, 
            double lat = -23.502555,
            long monthlyCondoFee = 300,
            long rentalTotalPrice = 3700) =>
            new()
            {
                UsableAreas = 4000,
                ListingType = "USED",
                CreatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                ListingStatus = "ACTIVE",
                Id = id,
                ParkingSpaces = 2,
                UpdatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Owner = false,
                Images = new List<string> { "url" },
                Address = new Address()
                {
                    City = "Belo Horizonte",
                    Neighborhood = "Contagem",
                    GeoLocation = new GeoLocation()
                    {
                        Precision = "ROOFTOP",
                        Location = new Location()
                        {
                            Lon = lon,
                            Lat = lat
                        }
                    }
                },
                Bathrooms = 2,
                Bedrooms = 3,
                PricingInfos = new PricingInfos()
                {
                    YearlyIptu = 500,
                    Price = 0,
                    RentalTotalPrice = rentalTotalPrice,
                    BusinessType = "RENT",
                    MonthlyCondoFee = monthlyCondoFee
                }
            };
    }
}
