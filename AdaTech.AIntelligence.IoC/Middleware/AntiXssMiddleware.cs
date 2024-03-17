using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using AngleSharp.Text;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ErrosCustomer;

namespace AdaTech.AIntelligence.IoC.Middleware
{
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
            if (IsImageRequest(context))
            {
                await _next(context);
                return;
            }

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

        private static bool IsImageRequest(HttpContext context)
        {
            var contentType = context.Request.ContentType;
            if (!string.IsNullOrEmpty(contentType) && (contentType.StartsWith("image/jpeg") || contentType.StartsWith("image/png") || contentType.StartsWith("image/gif")))
                return true;

            if (context.Request.HasFormContentType)
            {
                var form = context.Request.Form;
                foreach (var file in form.Files)
                {
                    var fileName = file.FileName;
                    var fileExtension = Path.GetExtension(fileName).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Imported from System.Web.CrossSiteScriptingValidation Class
    /// </summary>
    public static class CrossSiteScriptingValidation
    {
        private static readonly char[] StartingChars = { '<', '&' };

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

        public static void AddHeaders(this IHeaderDictionary headers)
        {
            if (headers["P3P"].IsNullOrEmpty())
            {
                headers.Append("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }

    public static class AntiXssMiddlewareExtension
    {
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleware>();
        }
    }
}
