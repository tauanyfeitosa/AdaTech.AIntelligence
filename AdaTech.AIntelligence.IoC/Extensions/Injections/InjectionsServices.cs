using AdaTech.AIntelligence.IoC.Middleware;
using AdaTech.AIntelligence.Service.Services.SeedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial;


namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{
    public static class InjectionsServices
    {
        public static IServiceCollection ResolveDependenciesService(this IServiceCollection services)
        {
            services.AddScoped<ISeedUserInitial, SeedUserInitial>();
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IExpenseScriptGPT, ExpenseScriptGPT>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IExpenseCRUDService, ExpenseCRUDService>();
            services.AddScoped<RoleManagerService, RoleManagerService>();
            services.AddScoped<UserManagerService, UserManagerService>();
            services.AddScoped(typeof(IDeleteStrategy<>), typeof(HardDeleteStrategy<>));
            services.AddScoped(typeof(IDeleteStrategy<>), typeof(SoftDeleteStrategy<>));
            services.AddScoped(typeof(IAIntelligenceRepository<>), typeof(AIntelligenceRepository<>));
            services.AddHttpClient();

            return services;
        }

        public static IApplicationBuilder ResolveDependenciesMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MiddlewareException>();
            return app;
        }
    }
}
