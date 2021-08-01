using Challenge.RealEstates.Infrastructure.Data.Interfaces;
using Challenge.RealEstates.Infrastructure.Repositories;
using Moq;
using System.Collections;
using System.Collections.Generic;
using Challenge.RealEstates.Domain.Entities;

namespace Challenge.RealEstates.Infrastruture.UnitTest.Repositories.Fixture
{
    public class RealEstateFixture
    {
        public readonly RealEstateRepository Repository;

        public RealEstateFixture()
        {
            Repository = new RealEstateRepository(InitializeDataInMemory());
        }

        private static IDataInMemory InitializeDataInMemory()
        {
            var mock = new Mock<IDataInMemory>();

            mock.Setup(m => m.Data).Returns(new Hashtable { { "123", new RealEstate { Id = "123" } } });
            mock.Setup(m => m.ZapIds).Returns(new HashSet<string>() { "123" });
            mock.Setup(m => m.VivaRealIds).Returns(new HashSet<string>());
            mock.Setup(m => m.Filters).Returns(new Dictionary<string, Dictionary<string, HashSet<string>>>());

            return mock.Object;
        }

        public static RealEstate GetRealEstate() =>
            new()
            {
                Id = "123",
                Address = new Address()
                {
                    City = "BH"
                },
                Bedrooms = 2,
                Bathrooms = 2,
                ParkingSpaces = 1,
                PricingInfos = new PricingInfos()
                {
                    BusinessType = "SALE"
                }
            };
    }
}
