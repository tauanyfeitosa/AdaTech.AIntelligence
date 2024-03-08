using AdaTech.AIntelligence.DateLibrary.Context;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AdaTech.AIntelligence.IoC
{
    public static class InjectionsDbContext
    {
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
