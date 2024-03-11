using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExpenseScriptGPT _expenseScriptGPT;
        private readonly IExpenseCRUDService _expenseCRUDService;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;


        private const string _url = "https://api.openai.com/v1/chat/completions";

        public ExpenseController(IConfiguration configuration, IWebHostEnvironment environment, 
            ILogger<ExpenseController> logger, IExpenseScriptGPT expenseScriptGPT, 
            IExpenseCRUDService expenseCRUDService)
        {
            _configuration = configuration;
            _logger = logger;
            _expenseScriptGPT = expenseScriptGPT;
            _expenseCRUDService = expenseCRUDService;
            _apiKey = _configuration.GetValue<string>("ApiKey");

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        [HttpPost("enviarImagemParaOChatGPT")]
        public async Task<IActionResult> EnviarImagemParaOChatGPT(IFormFile image)
        {
            var (urlImage, base64Image) = await image.DescriptionImage();

            var contentRequest = await _expenseScriptGPT.ExpenseScriptPrompt(base64Image, urlImage);

            var response = await _httpClient.PostAsync(_url, contentRequest);

            var resposta = await response.ProcessResponse();

            var success = await _expenseCRUDService.CreateExpense(resposta);

            if (!success)
                return BadRequest("Erro ao criar despesa.");

            return Ok("Despesa cadastrada com sucesso!");

        }
        [HttpPost("montarObjetoSobreAImagemEnviada")]
        public async Task<IActionResult> TesteDeRespostaDaImagem([FromQuery] string url)
        {
            var urlObject = await url.DescriptionImage();

            var contentRequest = await _expenseScriptGPT.ExpenseScriptPrompt(url, urlObject);

            var response = await _httpClient.PostAsync(_url, contentRequest);

            var resposta = await response.ProcessResponse();

            var success = await _expenseCRUDService.CreateExpense(resposta);

            if (!success)
                return BadRequest("Erro ao criar despesa.");

            return Ok("Despesa cadastrada com sucesso!");
        }
    }
}
