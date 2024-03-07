using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdaTech.AIntelligence.WebAPI.Filters
{
    public class MustHaveAToken : IAuthorizationFilter
    {
        private IHttpContextAccessor _HttpContextAccessor;
        public MustHaveAToken(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_HttpContextAccessor!.HttpContext!.Request.Cookies.ContainsKey("jwt"))
            {
                context.Result = new ContentResult()
                {
                    Content = "Permissão inválida",
                    StatusCode = 401
                };
                return;
            }
        }
    }
}
