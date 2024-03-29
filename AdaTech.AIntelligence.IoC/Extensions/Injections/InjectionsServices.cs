using AdaTech.AIntelligence.Service.Services.RoleRequirementService.PromotionServices;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService.StrategyDelete;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.ImageService;
using AdaTech.AIntelligence.Service.Services.SeedUser.SeedManagerInitial;
using AdaTech.AIntelligence.Service.Services.UserSystem.UserInterface;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.RoleRequirementService;
using AdaTech.AIntelligence.Service.Services.DeleteStrategyService;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.EmailService;
using AdaTech.AIntelligence.Service.Services.UserSystem;
using AdaTech.AIntelligence.Service.Services.SeedUser;
using AdaTech.AIntelligence.DbLibrary.Repository;
using Microsoft.Extensions.DependencyInjection;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.IoC.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;


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
            services.AddScoped<IUserCRUDService, UserCRUDService>();
            services.AddScoped<RoleManagerService, RoleManagerService>();
            services.AddScoped<UserManagerService, UserManagerService>();
            services.AddScoped<ResponseGPTService, ResponseGPTService>();
            services.AddScoped<GenericDeleteService<Expense>, GenericDeleteService<Expense>>();
            services.AddScoped<GenericDeleteService<UserInfo>, GenericDeleteService<UserInfo>>();
            services.AddScoped(typeof(IDeleteStrategy<>), typeof(HardDeleteStrategy<>));
            services.AddScoped(typeof(IDeleteStrategy<>), typeof(SoftDeleteStrategy<>));
            services.AddScoped(typeof(IAIntelligenceRepository<>), typeof(AIntelligenceRepository<>));
            services.AddScoped<PromotionService, PromotionService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<SmtpClient, SmtpClient>();
            services.AddScoped<RequirementService>();
            services.AddHttpClient();

            return services;
        }

        public static IApplicationBuilder ResolveDependenciesMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomAuthorizationMiddleware>();
            app.UseAntiXssMiddleware();
            app.UseMiddleware<MiddlewareException>();
            return app;
        }
    }
}
