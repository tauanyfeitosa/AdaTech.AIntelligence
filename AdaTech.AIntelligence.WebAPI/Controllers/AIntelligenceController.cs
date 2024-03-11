using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIntelligenceController : ControllerBase
    {
        private readonly ILogger<AIntelligenceController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExpenseScriptGPT _expenseScriptGPT;

        public AIntelligenceController(IConfiguration configuration, IWebHostEnvironment environment, 
            ILogger<AIntelligenceController> logger, IExpenseScriptGPT expenseScriptGPT)
        {
            _configuration = configuration;
            _logger = logger;
            _expenseScriptGPT = expenseScriptGPT;
        }
        [HttpPost("enviarImagemParaOChatGPT")]
        public async Task<IActionResult> EnviarImagemParaOChatGPT(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            var base64Image = await image.GetImage(extension);

            var apiKey = _configuration.GetValue<string>("ApiKey");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var imageContentType = extension == ".png" ? "image/png" : "image/jpeg";

            var urlImage = new
            {
                role = "user",
                content = new object[]
                        {
                            new { type = "text", text = "What’s in this image?" },
                            new { type = "image_url", image_url = $"data:image/{extension.Substring(1)};base64,{base64Image}" }
                        }
            };

            var contentRequest = await _expenseScriptGPT.ExpenseScriptPrompt(base64Image, urlImage);

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentRequest);

            var resposta = await response.ProcessResponse();

            return Ok(resposta);
        }
        [HttpPost("montarObjetoSobreAImagemEnviada")]
        public async Task<IActionResult> TesteDeRespostaDaImagem([FromQuery] string url)
        {
            var urlObject = new
            {
                role = "user",
                content = new object[]
                        {
                            new { type = "image_url", image_url = new { url = $"{url}" } }
                        }
            };

            var apiKey = _configuration.GetValue<string>("ApiKey");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var contentRequest = await _expenseScriptGPT.ExpenseScriptPrompt(url, urlObject);

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentRequest);

            var resposta =  await response.ProcessResponse();

            return Ok(resposta);
        }
    }
}
