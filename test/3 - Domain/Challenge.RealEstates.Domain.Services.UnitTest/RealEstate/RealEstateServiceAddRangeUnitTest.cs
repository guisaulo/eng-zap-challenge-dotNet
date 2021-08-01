using Challenge.RealEstates.Domain.Services.UnitTest.RealEstate.Fixture;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Challenge.RealEstates.Domain.Services.UnitTest.RealEstate
{
    public class RealEstateServiceAddRangeUnitTest : IClassFixture<RealEstateFixture>
    {
        private readonly RealEstateService _realEstateService;
        public RealEstateServiceAddRangeUnitTest(RealEstateFixture fixture)
        {
            _realEstateService = fixture.Service;
        }

        [Fact]
        public void Should_Return_Empty_Response_When_Add_RealEstate_With_Null_Input()
        {
            var result = _realEstateService.AddRange(null);
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(0));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(0));
            Assert.True(result.Zap.InvalidIds.Count.Equals(0));
            Assert.True(result.VivaReal.ValidIds.Count.Equals(0));
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0));
        }

        [Fact]
        public void Should_Return_Empty_Response_When_Add_RealEstate_With_Empty_Input()
        {
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>());
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(0));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(0));
            Assert.True(result.Zap.InvalidIds.Count.Equals(0));
            Assert.True(result.VivaReal.ValidIds.Count.Equals(0));
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0));
        }

        [Fact]
        public void Should_Return_Valid_Response_to_Zap_and_VivaReal_When_Add_RealEstate_to_Rental_With_Valid_Input()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToRental("X123");
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.Zap.InvalidIds.Count.Equals(0));
            Assert.True(result.VivaReal.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0));
        }

        [Fact]
        public void Should_Return_Valid_Response_to_Zap_and_VivaReal_When_Add_RealEstate_to_Sale_With_Valid_Input()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToSale("X123");
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.Zap.InvalidIds.Count.Equals(0));
            Assert.True(result.VivaReal.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0));
        }

        #region Zap portal exclusive business rules
        [Fact]
        public void Should_Return_Invalid_ZapResponse_When_Add_RealEstate_to_Rental_With_RentalPrice_Smaller_than_allowed()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToRental("X123", rentalTotalPrice: 1500);
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(0)); 
            Assert.True(result.Zap.InvalidIds.Count.Equals(1)); // Invalid RentalPrice
            Assert.True(result.VivaReal.ValidIds.Count.Equals(1)); //Eligible 
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0)); 
        }

        [Fact]
        public void Should_Return_Invalid_ZapResponse_When_Add_RealEstate_to_Sale_With_UsableAreas_is_Invalid()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToSale(id: "X123", usableAreas: 2000);
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(0));
            Assert.True(result.Zap.InvalidIds.Count.Equals(1)); //Invalid UsableAreas
            Assert.True(result.VivaReal.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0)); 
        }

        [Fact]
        public void Should_Return_Invalid_ZapResponse_When_Add_RealEstate_to_Sale_With_Price_Smaller_than_allowed_and_within_BoundingBox()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToSale(id: "X123", price: 40000, lat: -23.550000, lon: -46.650000);
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(0));
            Assert.True(result.Zap.InvalidIds.Count.Equals(1)); //Invalid Price - Smaller than allowed in BoundingBox
            Assert.True(result.VivaReal.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(0)); 
        }

        #endregion

        #region VivaReal portal exclusive business rules

        [Fact]
        public void Should_Return_Invalid_VivaRealResponse_When_Add_RealEstate_to_Rental_With_MonthlyCondoFee_Invalid()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToRental(id: "X123", monthlyCondoFee: 2500);
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.Zap.InvalidIds.Count.Equals(0));
            Assert.True(result.VivaReal.ValidIds.Count.Equals(0));
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(1)); //Invalid monthlyCondoFee 
        }

        [Fact]
        public void Should_Return_Invalid_VivaRealResponse_When_Add_RealEstate_to_Rental_With_RentalPrice_Bigger_than_allowed_and_within_BoundingBox()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToRental(id: "X123", rentalTotalPrice: 7000, lat: -23.550000, lon: -46.650000);
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.Zap.InvalidIds.Count.Equals(0)); 
            Assert.True(result.VivaReal.ValidIds.Count.Equals(0));
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(1)); //Invalid RentalPrice - Bigger than allowed in BoundingBox
        }

        [Fact]
        public void Should_Return_Invalid_VivaRealResponse_When_Add_RealEstate_to_Sale_With_Price_Bigger_than_allowed()
        {
            var realEstate = RealEstateFixture.GetValidRealEstateToSale(id: "X123", price: 800000);
            var result = _realEstateService.AddRange(new List<Entities.RealEstate>() { realEstate });
            result.Should().NotBeNull();
            Assert.True(result.Input.ValidIds.Count.Equals(1));
            Assert.True(result.Input.InvalidIds.Count.Equals(0));
            Assert.True(result.Zap.ValidIds.Count.Equals(1)); //Eligible
            Assert.True(result.Zap.InvalidIds.Count.Equals(0)); 
            Assert.True(result.VivaReal.ValidIds.Count.Equals(0)); 
            Assert.True(result.VivaReal.InvalidIds.Count.Equals(1)); //Invalid Price - Bigger than allowed
        }

        #endregion
    }
}