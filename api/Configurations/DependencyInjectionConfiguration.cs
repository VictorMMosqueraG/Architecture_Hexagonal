using Api.Configurations;
using Application;
using Infrastructure;

namespace Api.Configuration;

public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.LoadEnvironmentSettings();
            services.AddAppSettings(configuration);
            services.AddInfrastructure(configuration);
            services.AddBusiness();
            services.AddAuthentication();
            services.AddPolicies();

            return services;
        }
    }