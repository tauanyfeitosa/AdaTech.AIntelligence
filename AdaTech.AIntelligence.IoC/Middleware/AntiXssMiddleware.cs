using Microsoft.AspNetCore.Builder;
using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace AdaTech.AIntelligence.IoC.Middleware
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private ErrorResponse _error;
        private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Request.Body;
            try
            {
                var content = await ReadRequestBody(context);

                var sanitizer = new HtmlSanitizer();
                var sanitized = sanitizer.Sanitize(content);

                if (content != sanitized.Replace("&amp;", "&"))
                    await RespondWithAnError(context).ConfigureAwait(false);
            }
            finally
            {
                context.Request.Body = originalBody;
            }
        }

        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return requestContent;
        }

        private async Task RespondWithAnError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            context.Response.StatusCode = _statusCode;

            if (_error is null)
            {
                _error = new ErrorResponse
                {
                    Description = "XSS Detected.",
                    ErrorCode = 400
                };
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(_error));
        }
    }

    public static class AntiXssMiddlewareExtension
    {
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleware>();
        }
    }

    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
