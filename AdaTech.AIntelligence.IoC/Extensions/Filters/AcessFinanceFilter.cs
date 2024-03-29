﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace AdaTech.AIntelligence.IoC.Extensions.Filters
{
    /// <summary>
    /// Filter to control the access to the finance resources
    /// </summary>
    public class AcessFinanceFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var isFinanceUser = user.IsInRole("Finance");

            if (!isFinanceUser)
            {
                throw new UnauthorizedAccessException("Acesso negado");
            }
        }
    }

}
