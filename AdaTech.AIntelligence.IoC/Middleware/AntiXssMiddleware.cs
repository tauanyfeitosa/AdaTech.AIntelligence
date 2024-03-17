using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ErrosCustomer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AngleSharp.Text;
using System.Text;
using System.Net;

namespace AdaTech.AIntelligence.IoC.Middleware
{
    /// <summary>
    /// Middleware to prevent Cross-Site Scripting (XSS) attacks by validating request URLs and query strings.
    /// </summary>
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private ErrorDetails _error;
        private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(context.Request.Path.Value))
            {
                var url = context.Request.Path.Value;

                if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(context.Request.QueryString.Value))
            {
                var queryString = WebUtility.UrlDecode(context.Request.QueryString.Value);

                if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            var originalBody = context.Request.Body;
            try
            {
                var content = await ReadRequestBody(context);

                if (CrossSiteScriptingValidation.IsDangerousString(content, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
                await _next(context).ConfigureAwait(false);
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
            context.Response.Headers.AddHeaders();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = _statusCode;

            if (_error == null)
            {
                _error = new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "XSS detectado."
                };
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(_error));
        }
    }

    /// <summary>
    /// Imported from System.Web.CrossSiteScriptingValidation Class
    /// </summary>
    public static class CrossSiteScriptingValidation
    {
        private static readonly char[] StartingChars = { '<', '&' };

        /// <summary>
        /// Checks if a string contains potentially dangerous content indicating a Cross-Site Scripting (XSS) attack.
        /// </summary>
        /// <param name="request">The string to validate.</param>
        /// <param name="matchIndex">The index of the first character of the potentially dangerous content if found; otherwise, 0.</param>
        /// <returns>True if the string contains potentially dangerous content; otherwise, false.</returns>
        public static bool IsDangerousString(string request, out int matchIndex)
        {
            matchIndex = 0;

            for (var i = 0; i < request.Length; i++)
            {
                var n = request.IndexOfAny(StartingChars, i);
                if (n < 0) return false;

                if (n == request.Length - 1) return false;

                matchIndex = n;

                char letter = request[n];
                switch (letter)
                {
                    case '<':
                        if (letter.IsLetter() || request[n + 1] == '!' || request[n + 1] == '/' || request[n + 1] == '?')
                            return true;
                        break;
                    case '&':
                        if (request[n + 1] == '#')
                            return true;
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds the P3P header to the HTTP response headers if it's not already present.
        /// </summary>
        /// <param name="headers">The HTTP response headers.</param>
        public static void AddHeaders(this IHeaderDictionary headers)
        {
            if (headers["P3P"].IsNullOrEmpty())
            {
                headers.Append("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            }
        }

        /// <summary>
        /// Checks if a sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to check.</param>
        /// <returns>True if the sequence is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }

    /// <summary>
    /// Extension method to add the AntiXssMiddleware to the request pipeline.
    /// </summary>
    public static class AntiXssMiddlewareExtension
    {
        /// <summary>
        /// Adds the AntiXssMiddleware to the request pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleware>();
        }
    }
}
