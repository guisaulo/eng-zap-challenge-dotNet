using Challenge.RealEstates.Domain.Core.Interfaces.Repositories;
using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using Challenge.RealEstates.Domain.Services.UnitTest.RealEstate.Faker;
using FluentValidation.Results;
using System.Linq;

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

            mock.Setup(m => m.GetAllPaged(It.IsAny<PagedParams>(), It.IsAny<Filters>()))
                .Returns((PagedParams p, Filters f) => GetRealEstates(p, f));

            return mock.Object;
        }

        private static PagedResponse<Entities.RealEstate> GetRealEstates(PagedParams pagedParams, Filters filters)
        {
            if (filters?.Source == null)
                return new PagedResponse<Entities.RealEstate>();

            var query = filters.Source == "ZAP"
                ? RealEstateFaker.GetRealEstatesZapAsQueryable()
                : RealEstateFaker.GetRealEstatesVivaRealAsQueryable();

            query = GetByFilter(query, filters);

            var pageNumber = (pagedParams == null || pagedParams.PageNumber == 0) ? 1 : pagedParams.PageNumber;
            var pageSize = (pagedParams == null || pagedParams.PageSize == 0) ? 10 : pagedParams.PageSize;

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new PagedResponse<Entities.RealEstate>()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = query.Count(),
                Listings = query
            };
        }

        private static IQueryable<Entities.RealEstate> GetByFilter(IQueryable<Entities.RealEstate> query, Filters filters)
        {
            if (filters.City != null)
                query = query.Where(r => r.Address.City.Equals(filters.City));

            if (filters.BusinessType != null)
                query = query.Where(r => r.PricingInfos.BusinessType.Equals(filters.BusinessType));

            if (filters.Bedrooms != null)
                query = query.Where(r => r.Bedrooms == int.Parse(filters.Bedrooms));

            if (filters.Bathrooms != null)
                query = query.Where(r => r.Bathrooms == int.Parse(filters.Bathrooms));

            if (filters.ParkingSpaces != null)
                query = query.Where(r => r.ParkingSpaces == int.Parse(filters.ParkingSpaces));

            return query;
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

        public static Entities.RealEstate GetValidRealEstateToSale(string id, double lon = -46.716542, double lat = -23.502555, int usableAreas = 4000, long price = 650000) =>
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
                    Price = price,
                    RentalTotalPrice = 0,
                    BusinessType = "SALE",
                    MonthlyCondoFee = 500
                }
            };

        public static Entities.RealEstate GetValidRealEstateToRent(string id, double lon = -46.716542, double lat = -23.502555, long monthlyCondoFee = 300, long rentalTotalPrice = 3700) =>
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

        public static IEnumerable<object[]> GetQueryScenariosNullOrEmpty() =>
            new List<object[]>
            {
                new object[] { default(PagedParams), default(Filters) },
                new object[] { new PagedParams(), new Filters() }
            };

        public static IEnumerable<object[]> GetQueryScenarios() =>
            new List<object[]>
            {
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP" }, 5 },
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", City = "São Paulo"}, 0},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", City = "Belo Horizonte"}, 3},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", Bedrooms = "3" }, 4},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", Bathrooms = "2" }, 3},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", ParkingSpaces = "1" }, 4},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", BusinessType = "RENT"}, 2},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "ZAP", City = "Contagem", BusinessType = "RENT", Bathrooms = "2", Bedrooms = "2", ParkingSpaces = "0"}, 1},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL" }, 5 },
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", City = "São Paulo"}, 0},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", City = "Belo Horizonte"}, 3},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", Bedrooms = "3" }, 4},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", Bathrooms = "2" }, 3},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", ParkingSpaces = "1" }, 4},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", BusinessType = "RENT"}, 2},
                new object[] { new PagedParams(){ PageNumber = 1, PageSize = 10 }, new Filters() { Source = "VIVAREAL", City = "Contagem", BusinessType = "RENT", Bathrooms = "2", Bedrooms = "2", ParkingSpaces = "0"}, 1},
            };
    }
}
