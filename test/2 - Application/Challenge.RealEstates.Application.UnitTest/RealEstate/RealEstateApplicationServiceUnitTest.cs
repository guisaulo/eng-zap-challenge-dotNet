using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.UnitTest.RealEstate.Fixture;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Challenge.RealEstates.Application.UnitTest.RealEstate
{
    public class RealEstateApplicationServiceUnitTest : IClassFixture<RealEstateFixture>
    {
        private readonly RealEstateApplicationService _realEstateApplicationService;

        public RealEstateApplicationServiceUnitTest(RealEstateFixture fixture)
        {
            _realEstateApplicationService = fixture.Service;
        }

        [Fact]
        public void Should_AddRange_Successfully()
        {
            var realEstate = new List<Domain.Entities.RealEstate>() { new (){ Id = "123" } };
            var result = _realEstateApplicationService.AddRange(realEstate);
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(0));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(0));
            Assert.True(result.Zap.InvalidIds.Count.Equals(0));
            Assert.True(result.VivaReal.ValidIds.Count.Equals(0));
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0));
        }

        [Fact]
        public void Should_GetAllPaged_Successfully()
        {
            var result = _realEstateApplicationService.GetAllPaged("zap", new RealEstatesSearchDto());
            Assert.True(result.PageNumber.Equals(1));
            Assert.True(result.PageSize.Equals(10));
            Assert.True(result.TotalCount.Equals(1));
            Assert.True(result.Listings.Count().Equals(1));
        }
    }
}