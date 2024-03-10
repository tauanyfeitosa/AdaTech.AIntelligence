using AdaTech.AIntelligence.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIntelligenceController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AIntelligenceController> _logger;
        private readonly IConfiguration _configuration;

        public AIntelligenceController(IConfiguration configuration, IWebHostEnvironment environment, ITokenService tokenService, ILogger<AIntelligenceController> logger)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
        }

        //[HttpPost("criarToken")]
        //public IActionResult CriarToken(string user, string password)
        //{
        //    var token = _tokenService.GenerateToken(user, password);
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = System.DateTime.Now.AddHours(1)
        //    };
        //    Response.Cookies.Append("jwt", token, cookieOptions);
        //    return Ok(token);
        //}

        //[HttpPost("deletarToken")]
        //[ServiceFilter(typeof(MustHaveAToken))]
        //public IActionResult DeletarToken()
        //{
        //    Response.Cookies.Delete("jwt");
        //    return Ok("Token deletado");
        //}

        [HttpPost("enviarImagemParaOChatGPT")]
        public async Task<IActionResult> EnviarImagemParaOChatGPT([FromBody] string prompt)
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

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
                            new { type = "text", text = prompt },
                            new { type = "image_url", image_url = new { url = $"https://upload.wikimedia.org/wikipedia/commons/thumb/9/97/Nfe.png/480px-Nfe.png" } }
                        }
                    }
                }
            };

            var contentRequest = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentRequest);

            return await ProcessResponse(response);
        }
        [HttpPost("montarObjetoSobreAImagemEnviada")]
        public async Task<IActionResult> TesteDeRespostaDaImagem([FromBody] string prompt)
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

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
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "A imagem contém um comprovante fiscal? Continuar somente se a resposta for SIM, caso contrário, responder somente 'Imagem inválida'." },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "Responder em formato JSON" },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "Despesa: em que categoria está a despesa? entre: hospedagem, transporte, viagem, alimentação ou Outros." },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "Valor: qual o valor total da despesa?" },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "Descrição: descreva a despesa em no máximo 50 caracteres" },
                        }
                    },

                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = prompt },
                            new { type = "image_url", image_url = new { url = $"https://3.bp.blogspot.com/-XyNm0k6PDsU/WHFabVwMrlI/AAAAAAAAJ0o/-SF8xk8w2hstlvnjNp8-kybo5Zm3MnHtwCLcB/s1600/nota%2Bfiscal%2Bdotz.jpg" } }
                        }
                    }
                },
                max_tokens = 300
            };
            var contentRequest = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentRequest);

            return await ProcessResponse(response);
        }

        private async Task<IActionResult> ProcessResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return await HandleErrorResponse(response);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);
            var root = doc.RootElement;

            if (!root.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
            {
                return BadRequest("No choices found in the response.");
            }

            var firstChoice = choices[0];
            if (!firstChoice.TryGetProperty("message", out var message) || !message.TryGetProperty("content", out var content))
            {
                return BadRequest("Content property not found in the first choice.");
            }

            var contentString = content.GetString();
            Console.WriteLine(contentString);
            return Ok(contentString); 
        }

        private async Task<IActionResult> HandleErrorResponse(HttpResponseMessage response)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Error calling API: {errorContent}");
            return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} {errorContent}");
        }
    }
}
