using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using bART_Task.EF.Interfaces;
using bART_Task.EF.Services;
using bART_Task.EF.Repositories;

namespace bART_Task.EF
{
    public static class DIConfiguration
    {
        private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<bARTTaskContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLDatabase")));
        }

        private static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDbEntityService<>), typeof(DbEntityService<>));
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            RegisterDatabaseDependencies(services, configuration);
            RegisterServiceDependencies(services);
        }
    }
}
