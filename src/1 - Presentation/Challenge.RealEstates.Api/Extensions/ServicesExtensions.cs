using AutoMapper;
using Challenge.RealEstates.Application;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEstates.Application.Mappers;
using Challenge.RealEstates.Gateways;
using Challenge.RealEstates.Gateways.Interfaces;
using Challenge.RealEstates.Domain.Core.Interfaces.Repositories;
using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using Challenge.RealEstates.Services;
using Challenge.RealEstates.Infrastructure.Data;
using Challenge.RealEstates.Infrastructure.Data.Interfaces;
using Challenge.RealEstates.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Challenge.RealEstates.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateApplicationService, RealEstateApplicationService>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LocationProfile());
                mc.AddProfile(new GeoLocationProfile());
                mc.AddProfile(new AddressProfile());
                mc.AddProfile(new PricingInfosProfile());
                mc.AddProfile(new RealEstateProfile());
                mc.AddProfile(new PagedResponseProfile());
            });

            services.TryAddSingleton(mapperConfig.CreateMapper());
        }

        public static void AddGateways(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateGateway, RealEstateGateway>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateService, RealEstateService>();
            services.TryAddScoped<IRealEstateValidationService, RealEstateValidationService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateRepository, RealEstateRepository>();
        }

        public static void AddDataInMemory(this IServiceCollection services)
        {
            services.TryAddSingleton<IDataInMemory, DataInMemory>();
        }
    }
}