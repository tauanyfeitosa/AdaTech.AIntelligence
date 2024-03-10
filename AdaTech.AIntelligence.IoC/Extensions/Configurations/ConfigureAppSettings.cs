using AdaTech.AIntelligence.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdaTech.AIntelligence.IoC.Extensions.Configurations
{

    internal static class ConfigureAppSettings
    {
        /// <summary>
        /// Registra as classes de configuração do appsettings para uso injetado com IOptions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">provedor de configurações</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserCredentialsSettings>(configuration.GetSection("UserCredentials"));
            services.Configure<TokenSettings>(configuration.GetSection("TokenUserSettings"));

            return services;
        }
    }
}
