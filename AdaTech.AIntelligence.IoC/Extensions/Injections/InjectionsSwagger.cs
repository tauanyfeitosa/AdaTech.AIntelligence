using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Service.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
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

                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        var displayNameAttribute = controllerActionDescriptor.ControllerTypeInfo
                            .GetCustomAttributes(typeof(SwaggerDisplayNameAttribute), true)
                            .FirstOrDefault() as SwaggerDisplayNameAttribute;

                        if (displayNameAttribute != null)
                        {
                            return new[] { displayNameAttribute.DisplayName };
                        }
                    }

                    return new[] { api.ActionDescriptor.RouteValues["controller"] };
                });
                });

            return services;
        }
    }
}
