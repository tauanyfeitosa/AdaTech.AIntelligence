using AdaTech.AIntelligence.IoC.Extensions.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{
    public static class InjectionsSwagger
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Artificial Intelligence", Version = "v1" });


                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}
