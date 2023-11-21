using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.Refactoring.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InfoTrackDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("InfoTrackManagementConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();          
                      

            return services;    
        }
    }
}
