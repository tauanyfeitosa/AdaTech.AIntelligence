using AdaTech.AIntelligence.IoC.Extensions.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using AdaTech.AIntelligence.Attributes;
using Microsoft.OpenApi.Models;

namespace AdaTech.AIntelligence.IoC.Extensions.Injections
{
    /// <summary>
    /// Intended to inject swagger dependencies
    /// </summary>
    public static class InjectionsSwagger
    {

        /// <summary>
        /// Injects swagger documentation to accept XML documentation, access filters and custom tags to change controller names
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Artificial Intelligence", Version = "v1" });


                c.OperationFilter<SecurityRequirementsOperationFilter>();

                var xmlFile = $"AdaTech.AIntelligence.WebAPI.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

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
