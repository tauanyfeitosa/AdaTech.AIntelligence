using AdaTech.AIntelligence.IoC.Extensions.ApplicationInitializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using AdaTech.AIntelligence.IoC.Extensions.Injections;
using AdaTech.AIntelligence.IoC.Extensions.Configurations;

[assembly: InternalsVisibleTo("AdaTech.AIntelligence.WebAPI")]
namespace AdaTech.AIntelligence.IoC.Extensions
{
    /// <summary>
    /// Destinado a injetar as dependências do ciclo de vida da solution.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static class DependencyInjection
    {

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
