using AdaTech.AIntelligence.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public ChatGPTController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }


        [HttpGet("check")]
        public async Task<IActionResult> CheckGptConnection()
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");

            var response = await apiKey.GenerateResponse(_clientFactory);

            return Ok(response);
        }
    }
}
