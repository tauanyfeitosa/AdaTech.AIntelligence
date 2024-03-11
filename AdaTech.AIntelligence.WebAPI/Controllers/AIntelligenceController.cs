using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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
            if (image == null || image.Length == 0)
            {
                return BadRequest("Upload a valid image file.");
            }

            var allowedExtensions = new[] { ".jpg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Only .jpg and .png file formats are allowed.");
            }

            string base64Image;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }

            var apiKey = _configuration.GetValue<string>("ApiKey");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var imageContentType = extension == ".png" ? "image/png" : "image/jpeg";
            var requestData = new
            {
                model = "gpt-4-vision-preview",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "A resposta deve ser em português." },
                        }
                    },
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = "What’s in this image?" },
                            new { type = "image_url", image_url = $"data:image/{extension.Substring(1)};base64,{base64Image}" }
                        }
                    }
                }
            };


            var contentRequest = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentRequest);

            return Ok(await response.ProcessResponse());
        }
        [HttpPost("montarObjetoSobreAImagemEnviada")]
        public async Task<IActionResult> TesteDeRespostaDaImagem([FromQuery] string url)
        {

            var apiKey = _configuration.GetValue<string>("ApiKey");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var contentRequest = await _expenseScriptGPT.ExpenseScriptPrompt(url);

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentRequest);

            var resposta =  await response.ProcessResponse();

            return Ok(resposta);
        }
    }
}
