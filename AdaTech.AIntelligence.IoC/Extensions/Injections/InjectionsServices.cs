using AdaTech.AIntelligence.IoC.Middleware;
using AdaTech.AIntelligence.Service.Services.SeedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.DateLibrary.Repository;
using AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services.EmailService;
using System.Net.Mail;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.ResponseService;


namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{

    /// <summary>
    /// Injects the services dependencies
    /// </summary>
    public static class InjectionsServices
    {

        /// <summary>
        /// Extension method to resolve the services dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ResolveDependenciesService(this IServiceCollection services)
        {
            services.AddScoped<ISeedUserInitial, SeedUserInitial>();
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IExpenseCRUDService, ExpenseCRUDService>();
            services.AddScoped<RoleManagerService, RoleManagerService>();
            services.AddScoped<UserManagerService, UserManagerService>();
            services.AddScoped<ResponseGPTService, ResponseGPTService>();
            services.AddScoped<GenericDeleteService<Expense>, GenericDeleteService<Expense>>();
            services.AddScoped(typeof(IDeleteStrategy<>), typeof(HardDeleteStrategy<>));
            services.AddScoped(typeof(IDeleteStrategy<>), typeof(SoftDeleteStrategy<>));
            services.AddScoped(typeof(IAIntelligenceRepository<>), typeof(AIntelligenceRepository<>));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<SmtpClient, SmtpClient>();
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
