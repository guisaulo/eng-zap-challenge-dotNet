using Challenge.RealEstates.Application;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Services;
using Challenge.RealStates.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Challenge.RealEstates.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddScoped<IZapApplicationService, ZapApplicationService>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IZapService, ZapService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IZapRepository, ZapRepository>();
        }
    }
}