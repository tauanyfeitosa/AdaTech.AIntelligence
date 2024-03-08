using AdaTech.AIntelligence.DateLibrary.Context;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Ioc.Filters;
using AdaTech.AIntelligence.IoC.Extensions.ApplicationInitializer;
using AdaTech.AIntelligence.IoC.Middleware;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.SeedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;


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
                    .AddCustomConfiguration(configuration);
                    //.AddCustomSwagger()
                    //.AddSerilog(LoggingConfiguration.ConfigureSerilog(configuration));


            services.AddHostedService<StartupHostedApplication>();

            return services;
        }

        private static IServiceCollection ResolveDependenciesService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<MustHaveAToken>();
            services.AddScoped<ISeedUserInitial, SeedUserInitial>();

            return services;
        }

        public static IApplicationBuilder ResolveDependenciesMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MiddlewareException>();
            return app;
        }

        private static IServiceCollection ResolveDependenciesDbContext(this IServiceCollection services) 
        {
            services.AddDbContext<ExpenseReportingDbContext>();
            services.AddIdentity<UserInfo, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ExpenseReportingDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
