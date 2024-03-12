using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

            if (resposta.Contains("message"))

                return BadRequest(resposta);

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

            if (resposta.Contains("message"))

                return BadRequest(resposta);

            var success = await _expenseCRUDService.CreateExpense(resposta);

            if (!success)
                return BadRequest("Erro ao criar despesa.");

            return Ok("Despesa cadastrada com sucesso!");
        }

        [HttpPatch("alterarStatusDaDespesa")]
        public async Task<IActionResult> AlterarStatusDaDespesa([FromQuery] int idExpense)
        {
            var expense = await _expenseCRUDService.GetOne(idExpense);

            if(expense.Status == ExpenseStatus.PAGO)
                throw new NotAnExpenseException("Despesa n�o encontrada.");

            expense.Status = ExpenseStatus.PAGO;

            var success = await _expenseCRUDService.UpdateExpense(expense);

            if (!success)
                throw new Exception("Erro ao alterar o status da despesa.");

            return Ok("Status da despesa atualizado com sucesso!");
        }

        [HttpGet("VisualizarTodasDespesas")]
        [Authorize(Roles = "Admin")]
        [TypeFilter(typeof(AcessFinanceFilter))]
        public async Task<IActionResult> VisualizarTodasDespesas()
        {
            var success = await _expenseCRUDService.GetAll();

            if (success.IsNullOrEmpty())
                throw new NotFoundException("N�o existem despesas.");

            return Ok(success);
        }

        [HttpGet("VisualizarTodasDespesaAtivas")]
        [Authorize(Roles = "Admin")]
        [TypeFilter(typeof(AcessFinanceFilter))]
        public async Task<IActionResult> VisualizarTodasDespesaAtivas()
        {
            var success = await _expenseCRUDService.GetAllActive();

            if (success.IsNullOrEmpty())
                throw new NotFoundException("N�o existem despesas ativas.");

            return Ok(success);
        }

        [HttpGet("VisualizarTodasDespesasSubmetidas")]
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> VisualizarTodasDespesasSubmetidas()
        {
            var success = await _expenseCRUDService.GetAllSubmetido();

            if (success.IsNullOrEmpty())
                throw new NotFoundException();

            return Ok(success);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id, [FromQuery] bool isHardDelete = false)
        {
            var result = await _expenseCRUDService.DeleteAsync(id, isHardDelete);
            return Ok(result);
        }
    }
}
