using AdaTech.AIntelligence.Ioc.Filters;
using AdaTech.AIntelligence.IoC.Middleware;
using AdaTech.AIntelligence.Service.Services.SeedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Service.Services.UserSystem;


namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{
    public static class InjectionsServices
    {
        public static IServiceCollection ResolveDependenciesService(this IServiceCollection services)
        {
            services.AddScoped<MustHaveAToken>();
            services.AddScoped<ISeedUserInitial, SeedUserInitial>();
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IExpenseScriptGPT, ExpenseScriptGPT>();
            services.AddHttpClient();

            return services;
        }

        public static IApplicationBuilder ResolveDependenciesMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MiddlewareException>();
            return app;
        }

        public static IServiceCollection ResolveDependencieTokens(this IServiceCollection services)
        {
            services.AddAuthentication(
                config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(config =>
                    {
                        config.RequireHttpsMetadata = false;
                        config.SaveToken = true;
                        config.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("3h9RtE2F#pW!b5Z^Kx)vDc6S7GyP4NqX")),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidIssuer = "AIntelligence_issuer",
                            ValidAudience = "AIntelligence_users",
                            ValidateLifetime = true
                        };
                    });

            return services;
        }
    }
}
