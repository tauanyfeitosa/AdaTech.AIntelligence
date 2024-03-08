using AdaTech.AIntelligence.IoC.Extensions.ApplicationInitializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using AdaTech.AIntelligence.IoC;

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
                    .ResolveDependencieTokens()
                    .AddCustomConfiguration(configuration)
                    .AddCustomSwagger();
            //.AddSerilog(LoggingConfiguration.ConfigureSerilog(configuration));


            services.AddHostedService<StartupHostedApplication>();

            return services;
        }

       
   

    }
}
