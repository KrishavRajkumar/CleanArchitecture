using InfoTrack.Refactoring.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.Refactoring.Services
{
    public static class InfoTrackWCFServicesRegistrations
    {
        public static IServiceCollection AddInfoTrackWCFServices(this IServiceCollection services)
        {
            services.AddTransient<CandidateCreditServiceClientFactory>();
            services.AddScoped<ICandidateCreditService,CandidateCreditService>();
            return services;
        }
    }
}
