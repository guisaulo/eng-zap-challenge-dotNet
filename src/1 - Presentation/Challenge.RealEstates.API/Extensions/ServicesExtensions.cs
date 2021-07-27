using Challenge.RealEstates.Application;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEstates.Gateways;
using Challenge.RealEstates.Gateways.Interfaces;
using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Services;
using Challenge.RealStates.Infrastructure;
using Challenge.RealStates.Infrastructure.Data;
using Challenge.RealStates.Infrastructure.Data.Interfaces;
using Challenge.RealStates.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Challenge.RealEstates.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateApplicationService, RealEstateApplicationService>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateService, RealEstateService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateRepository, RealEstateRepository>();
        }

        public static void AddDataInMemory(this IServiceCollection services)
        {
            services.TryAddSingleton<IDataInMemory, DataInMemory>();
        }

        public static void AddGateways(this IServiceCollection services)
        {
            services.TryAddScoped<IRealEstateGateway, RealEstateGateway>();
        }
    }
}