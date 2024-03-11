using Microsoft.AspNetCore.Mvc.Filters;

namespace AdaTech.AIntelligence.IoC.Extensions.Filters
{
    public class AcessAdminFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            
            var isSuperUser = user.IsInRole("Admin");

            if (!isSuperUser)
            {
                throw new UnauthorizedAccessException("Acesso negado");
            }
        }
    }
}
