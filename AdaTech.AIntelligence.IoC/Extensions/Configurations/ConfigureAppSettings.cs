using Microsoft.Extensions.DependencyInjection;
using AdaTech.AIntelligence.Configuration;
using Microsoft.Extensions.Configuration;

namespace AdaTech.AIntelligence.IoC.Extensions.Configurations
{
    /// <summary>
    /// Class responsable for adding custom configuration at service collection.
    /// </summary>
    internal static class ConfigureAppSettings
    {
        /// <summary>
        /// Registers the appsettings classes for injected usage with IOptions.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <param name="configuration">The configuration provider.</param>
        /// <returns>The updated collection of services.</returns>
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserCredentialsSettings>(configuration.GetSection("UserCredentials"));

            return services;
        }
    }
}
