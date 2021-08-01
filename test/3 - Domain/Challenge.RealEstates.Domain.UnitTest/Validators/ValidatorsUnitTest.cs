using System.Collections.Generic;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Validators;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Challenge.RealEstates.Domain.UnitTest.Validators
{
    public class ValidatorsUnitTest
    {
        [Fact]
        public void Should_have_error_when_lat_and_lon_are_zero()
        {
            var validator = new LocationValidator();
            var result = validator.Validate(new Location { Lat = 0, Lon = 0 });
            result.IsValid.Should().BeFalse();
            result.Errors.Any(o => o.ErrorMessage == "'Lat' must not be equal to '0'.");
            result.Errors.Any(o => o.ErrorMessage == "'Lon' must not be equal to '0'.");
        }

        [Fact]
        public void Should_not_have_error_when_lat_and_lon_are_valid()
        {
            var validator = new LocationValidator();
            var result = validator.Validate(new Location { Lat = -24.00000, Lon = -25.00000 });
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_have_error_when_precision_is_invalid()
        {
            var validator = new GeoLocationValidator();
            var result = validator.Validate(new GeoLocation { Precision = "" });
            result.IsValid.Should().BeFalse();
            result.Errors.Any(o => o.ErrorMessage == "'Precision' must not be empty.'");
        }

        [Fact]
        public void Should_not_have_error_when_geolocation_is_valid()
        {
            var validator = new GeoLocationValidator();
            var result = validator.Validate(new GeoLocation { Precision = "123" });
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_have_error_when_address_is_invalid()
        {
            var validator = new AddressValidator();
            var result = validator.Validate(new Address { City = null, Neighborhood = null });
            result.IsValid.Should().BeFalse();
            result.Errors.Any(o => o.ErrorMessage == "'City' must not be empty.");
            result.Errors.Any(o => o.ErrorMessage == "'Neighborhood' must not be empty.");
        }

        [Fact]
        public void Should_not_have_error_when_address_is_valid()
        {
            var validator = new AddressValidator();
            var result = validator.Validate(new Address { City = "BH", Neighborhood = "SP" });
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_have_error_when_princeinfos_are_invalid()
        {
            var validator = new PricingInfosValidator();
            var result = validator.Validate(new PricingInfos { BusinessType = null, Price = 0 });
            result.IsValid.Should().BeFalse();
            result.Errors.Any(o => o.ErrorMessage == "'BusinessType' must not be empty.");
        }

        [Fact]
        public void Should_not_have_error_when_princeinfos_are_nvalid()
        {
            var validator = new PricingInfosValidator();
            var result = validator.Validate(new PricingInfos { BusinessType = "RENTAL", Price = 0 });
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_have_error_when_realestate_is_invalid()
        {
            var validator = new RealEstateValidator();
            var result = validator.Validate(new RealEstate { Id = "", CreatedAt = "", ListingStatus = "", ListingType = "", UpdatedAt = "", Images = null });
            result.IsValid.Should().BeFalse();
            result.Errors.Any(o => o.ErrorMessage == "'Id' must not be empty.");
            result.Errors.Any(o => o.ErrorMessage == "'CreatedAt' must not be empty.");
            result.Errors.Any(o => o.ErrorMessage == "'ListingStatus' must not be empty.");
            result.Errors.Any(o => o.ErrorMessage == "'ListingType' must not be empty.");
            result.Errors.Any(o => o.ErrorMessage == "'UpdatedAt' must not be empty.");
            result.Errors.Any(o => o.ErrorMessage == "'Images' must not be empty.");
        }

        [Fact]
        public void Should_not_have_error_when_realestate_is_valid()
        {
            var validator = new RealEstateValidator();
            var result = validator.Validate(new RealEstate { Id = "12", CreatedAt = "2021", ListingStatus = "TEST", ListingType = "TEST", UpdatedAt = "2021", Images = new List<string> { "2" } });
            result.IsValid.Should().BeTrue();
        }
    }
}
