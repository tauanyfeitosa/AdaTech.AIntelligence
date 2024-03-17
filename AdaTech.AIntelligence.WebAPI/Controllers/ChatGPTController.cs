using AdaTech.AIntelligence.Attributes;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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
        private readonly ILogger<ChatGPTController> _logger;

        public ChatGPTController(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<ChatGPTController> logger)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _logger = logger;
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

            string path = _configuration.GetValue<string>("BaseOCRUrl");
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
            }
            else
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }

    }
}
