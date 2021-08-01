using Challenge.RealEstates.Domain.Core.Interfaces.Repositories;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using Challenge.RealEstates.Infrastruture.UnitTest.Repositories.Fixture;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Challenge.RealEstates.Infrastruture.UnitTest.Repositories
{
    public class RealEstateRepositoryUnitTest : IClassFixture<RealEstateFixture>
    {
        private readonly IRealEstateRepository _realEstateService;
        public RealEstateRepositoryUnitTest(RealEstateFixture fixture)
        {
            _realEstateService = fixture.Repository;
        }

        [Fact]
        public void Should_add_viva_real_real_estate_successfully()
        {
            try
            {
                _realEstateService.AddVivaRealEstate(RealEstateFixture.GetRealEstate());
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Should_add_zap_real_estate_successfully()
        {
            try
            {
                _realEstateService.AddZapRealEstate(RealEstateFixture.GetRealEstate());
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Should_GetAllPaged_Successfully()
        {
            var result = _realEstateService.GetAllPaged(new PagedParams() { PageNumber = 1, PageSize = 10 }, new Filters { Source = "zap" });
            result.Should().NotBeNull();
            Assert.True(result.PageSize.Equals(10));
            Assert.True(result.PageNumber.Equals(1));
            Assert.True(result.TotalCount.Equals(1));
            Assert.True(result.Listings.Count().Equals(1));
        }
    }
}
