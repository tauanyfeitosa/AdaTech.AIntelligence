using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ErrosCustomer;
using System.Net.Mail;

namespace AdaTech.AIntelligence.IoC.Middleware
{

    /// <summary>
    /// Middleware to handle exceptions
    /// </summary>
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke the middleware
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            int originalStatusCode = httpContext.Response.StatusCode;

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Algo de errado aconteceu: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handle exceptions according to the custom exception received
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
           
            context.Response.ContentType = "application/json; charset=utf-8";


            var statusCode = exception switch
            {
                InvalidAmountException _ => StatusCodes.Status400BadRequest,
                InvalidCategoryException _ => StatusCodes.Status400BadRequest,
                InvalidDescriptionException _ => StatusCodes.Status400BadRequest,
                NotAnExpenseException _ => StatusCodes.Status400BadRequest,
                NotFoundException _ => StatusCodes.Status404NotFound,
                NotReadableImageException _ => StatusCodes.Status415UnsupportedMediaType,
                ReadingAmountException _ => StatusCodes.Status404NotFound,
                ReadingCategoryException _ => StatusCodes.Status404NotFound,
                ReadingDescriptionException _ => StatusCodes.Status404NotFound,
                NotConnectionGPTException _ => StatusCodes.Status404NotFound,
                UnprocessableEntityException _ => StatusCodes.Status422UnprocessableEntity,
                SmtpException _ => StatusCodes.Status500InternalServerError,
                FormatException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            var message = exception.Message;

            context.Response.StatusCode = statusCode;

            var errorResponse = new ErrorDetails
            {

                StatusCode = statusCode,
                Message = message
            };

            var erro = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(erro);
        }
    }
}
