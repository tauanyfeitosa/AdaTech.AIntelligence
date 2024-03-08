using AdaTech.AIntelligence.Service.Exceptions;

namespace AdaTech.AIntelligence.WebAPI.Utils.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                InvalidAmountException _ => StatusCodes.Status400BadRequest,
                InvalidCategoryException _ => StatusCodes.Status400BadRequest,
                InvalidDescriptionException _ => StatusCodes.Status400BadRequest,
                NotAnExpenseException _ => StatusCodes.Status400BadRequest,
                NotReadableImageException _ => StatusCodes.Status415UnsupportedMediaType,
                ReadingAmountException _ => StatusCodes.Status404NotFound,
                ReadingCategoryException _ => StatusCodes.Status404NotFound,
                ReadingDescriptionException _ => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            var message = exception.Message;

            context.Response.StatusCode = statusCode;

            var errorResponse = new ErrorDetails
            {
                StatusCode = statusCode,
                Message = message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
