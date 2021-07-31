using System.Linq;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using Challenge.RealEstates.Domain.Services.UnitTest.RealEstate.Fixture;
using FluentAssertions;
using Xunit;

namespace Challenge.RealEstates.Domain.Services.UnitTest.RealEstate
{
    public class RealEstateServiceGetAllPagedUnitTest : IClassFixture<RealEstateFixture>
    {
        private readonly RealEstateService _realEstateService;
        public RealEstateServiceGetAllPagedUnitTest(RealEstateFixture fixture)
        {
            _realEstateService = fixture.Service;
        }

        [Theory]
        [MemberData(nameof(RealEstateFixture.GetQueryScenariosNullOrEmpty), MemberType = typeof(RealEstateFixture))]
        public void Should_Run_GetAllPaged_And_Return_Expected_Result_When_Params_are_Null_or_Empty(PagedParams pagedParams, Filters filters)
        {
            var result = _realEstateService.GetAllPaged(pagedParams, filters);
            result.Should().NotBeNull();
            Assert.True(result.PageSize.Equals(0));
            Assert.True(result.PageNumber.Equals(0));
            result.Listings.Should().BeNullOrEmpty();
        }

        [Theory]
        [MemberData(nameof(RealEstateFixture.GetQueryScenarios), MemberType = typeof(RealEstateFixture))]
        public void Should_Run_GetAllPaged_And_Return_Expected_Result_Count(PagedParams pagedParams, Filters filters, int totalCount)
        {
            var result = _realEstateService.GetAllPaged(pagedParams, filters);
            result.Should().NotBeNull();
            result.PageNumber.Should().Be(pagedParams.PageNumber);
            result.PageSize.Should().Be(pagedParams.PageSize);
            result.TotalCount.Should().Be(totalCount);
            result.Listings.Count().Should().Be(totalCount);
        }

        [Fact]
        public void Should_Run_GetAllPaged_And_Return_Expected_Result_Count_When_Paged_Params_is_Null()
        {
            var result = _realEstateService.GetAllPaged(default(PagedParams), new Filters() { Source = "ZAP" });
            result.Should().NotBeNull();
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalCount.Should().Be(5);
            result.Listings.Count().Should().Be(5);
        }
    }
}