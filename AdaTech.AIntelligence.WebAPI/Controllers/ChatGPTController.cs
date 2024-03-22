using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using Microsoft.AspNetCore.Authorization;
using AdaTech.AIntelligence.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("Chat GPT - Vision")]
    [TypeFilter(typeof(LoggingActionFilter))]
    public class ChatGPTController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public ChatGPTController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Check if the GPT connection is working
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("check")]
        public async Task<IActionResult> CheckGptConnection()
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");

            string path = _configuration.GetValue<string>("BaseOCRUrl")!;
            string ocrApiUrl = $"{path}api/OCRChatGPT/check";

            var builder = new UriBuilder(ocrApiUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["apiKey"] = apiKey;
            builder.Query = query.ToString();
            string urlWithParams = builder.ToString();

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(urlWithParams);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);

            } else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new UnauthorizedAccessException("Acesso negado, verifique sua apiKey");
            }
            else
            {
                throw new NotConnectionGPTException("A coneção com o GPT não conseguiu ser estabelecida.");
            }
        }

    }
}
