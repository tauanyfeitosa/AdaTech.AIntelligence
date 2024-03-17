using AdaTech.AIntelligence.IoC.Extensions.ApplicationInitializer;
using AdaTech.AIntelligence.IoC.Extensions.Configurations;
using AdaTech.AIntelligence.IoC.Extensions.Injections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.WebAPI")]
namespace AdaTech.AIntelligence.IoC.Extensions
{
    /// <summary>
    /// Intended to inject solution lifecycle dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static class DependencyInjection
    {

        /// <summary>
        /// Extension method to resolve the services dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ResolveDependenciesService()
                    .ResolveDependenciesDbContext()
                    .AddCustomConfiguration(configuration)
                    .AddCustomSwagger();


            services.AddHostedService<StartupHostedApplication>();

            return services;
        }

    }
}
