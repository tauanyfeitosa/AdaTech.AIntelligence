using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Service.Attributes;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.ExpenseServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.ChatGPTServices;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("Report Expense")]
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

        /// <summary>
        /// Create an expense from an image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("create-expense-image-file")]
        public async Task<IActionResult> CreateExpenseImageFile(IFormFile image)
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

        /// <summary>
        /// Create an expense from an image url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("create-expense-image-url")]
        public async Task<IActionResult> CreateExpenseImageUrl([FromQuery] string url)
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

        /// <summary>
        /// Update the status of an expense
        /// </summary>
        /// <param name="idExpense"></param>
        /// <returns></returns>
        [Authorize(Roles = "Finance")]
        [HttpPatch("update-status-expense")]
        public async Task<IActionResult> UpdateStatusExpense([FromQuery] int idExpense)
        {
            var expense = await _expenseCRUDService.GetOne(idExpense);

            if(expense.Status == ExpenseStatus.PAID)
                throw new NotAnExpenseException("Despesa não encontrada.");

            expense.Status = ExpenseStatus.PAID;

            var success = await _expenseCRUDService.UpdateExpense(expense);

            if (!success)
                throw new Exception("Erro ao alterar o status da despesa.");

            return Ok("Status da despesa atualizado com sucesso!");
        }

        /// <summary>
        /// View all expenses (actives and inactives)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("view-expense")]
        [Authorize(Roles = "Admin")]
        [TypeFilter(typeof(AcessFinanceFilter))]
        public async Task<IActionResult> ViewExpenseAll()
        {
            var success = await _expenseCRUDService.GetAll();

            if (success.IsNullOrEmpty())
                throw new NotFoundException("Não existem despesas.");

            return Ok(success);
        }

        /// <summary>
        /// View all active expenses
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("view-expense-active")]
        [Authorize(Roles = "Admin")]
        [TypeFilter(typeof(AcessFinanceFilter))]
        public async Task<IActionResult> ViewExpenseActive()
        {
            var success = await _expenseCRUDService.GetAllActive();

            if (success.IsNullOrEmpty())
                throw new NotFoundException("N�o existem despesas ativas.");

            return Ok(success);
        }

        /// <summary>
        /// View active submitted expenses
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("view-expense-submitted")]
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> ViewExpenseSubmitted()
        {
            var success = await _expenseCRUDService.GetAllSubmitted();

            if (success.IsNullOrEmpty())
                throw new NotFoundException();

            return Ok(success);
        }

        /// <summary>
        /// Delete an expense in hard or soft mode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isHardDelete"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [TypeFilter(typeof(AcessFinanceFilter))]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteExpense(int id, [FromQuery] bool isHardDelete = false)
        {
            var result = await _expenseCRUDService.DeleteAsync(id, isHardDelete);
            return Ok(result);
        }
    }
}
