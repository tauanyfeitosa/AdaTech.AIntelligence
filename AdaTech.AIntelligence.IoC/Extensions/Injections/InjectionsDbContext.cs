using AdaTech.AIntelligence.DateLibrary.Context;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{
    public static class InjectionsDbContext
    {
        public static IServiceCollection ResolveDependenciesDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ExpenseReportingDbContext>();
            services.AddDbContext<IdentityDbContext<UserInfo>>();
            services.AddIdentity<UserInfo, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ExpenseReportingDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
