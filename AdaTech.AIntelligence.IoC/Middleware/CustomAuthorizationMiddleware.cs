using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ErrosCustomer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AdaTech.AIntelligence.IoC.Middleware
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorDetails() 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Usuário não autorizado para acessar esta rota."
                };

                context.Response.StatusCode = errorResponse.StatusCode;

                await context.Response.WriteAsync($"{JsonConvert.SerializeObject(errorResponse)}");
            }
        }
    }
}
