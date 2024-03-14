using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Service.Attributes;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.ChatGPTServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("Chat GPT - Vision")]
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

            var response = await apiKey.GenerateResponse(_clientFactory);

            return Ok(response);
        }
    }
}
