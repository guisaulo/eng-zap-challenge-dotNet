using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.RealEstates.Application.Mappers;
using Xunit;

namespace Challenge.RealEstates.Application.UnitTest.Mappers
{
    public class AllMappersUnitTest
    {
        [Fact]
        public void Should_to_validate_all_profiles()
        {
            var mappingTests = new MapperConfiguration(cfg => {
                cfg.AddProfile<LocationProfile>();
                cfg.AddProfile<GeoLocationProfile>();
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<PricingInfosProfile>();
                cfg.AddProfile<RealEstateProfile>();
                cfg.AddProfile<PagedResponseProfile>();
            }).CreateMapper();

            mappingTests.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
