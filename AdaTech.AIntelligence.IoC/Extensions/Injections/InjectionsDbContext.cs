using Microsoft.Extensions.DependencyInjection;
using AdaTech.AIntelligence.DbLibrary.Context;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;

namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{

    /// <summary>
    /// Intended to inject the dependencies of the DbContext
    /// </summary>
    public static class InjectionsDbContext
    {

        /// <summary>
        /// Extension method to resolve the DbContext dependencies e Identity dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ResolveDependenciesDbContext(this IServiceCollection services)
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
