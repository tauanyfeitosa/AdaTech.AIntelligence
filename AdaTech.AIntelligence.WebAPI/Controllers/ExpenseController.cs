using AdaTech.AIntelligence.Service.Services.ExpenseServices.ImageService;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.WebAPI.SistemaVendas.Utilities.Filters;
using AdaTech.AIntelligence.IoC.Extensions.Filters;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using AdaTech.AIntelligence.Attributes;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDisplayName("Report Expense")]
    [TypeFilter(typeof(LoggingActionFilter))]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IExpenseCRUDService _expenseCRUDService;
        private readonly HttpClient _httpClient;
        private readonly ResponseGPTService _responseGPTService;
        private readonly string _apiKey;
        private readonly UserManager<UserInfo> _userManager;

        public ExpenseController(IConfiguration configuration, ILogger<ExpenseController> logger, 
            IExpenseCRUDService expenseCRUDService, ResponseGPTService responseGPTService, 
            UserManager<UserInfo> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _expenseCRUDService = expenseCRUDService;
            _userManager = userManager;
            _apiKey = _configuration.GetValue<string>("ApiKey");
            _responseGPTService = responseGPTService;

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
            string path = _configuration.GetValue<string>("BaseOCRUrl");
            string ocrApiUrl = $"{path}api/OCRChatGPT/create-expenseRequest-image-file";

            var (extension, base64Image) = await image.DescriptionImage();

            var requestImage = new
            {
                Base64Image = base64Image,
                Extension = extension,
                ApiKey = _apiKey,
                Url = ""
            };

            var response = await _responseGPTService.GetResponseGPT(ocrApiUrl, requestImage, await _userManager.GetUserAsync(User));

            return Ok(response);
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
            string path = _configuration.GetValue<string>("BaseOCRUrl");
            string ocrApiUrl = $"{path}api/OCRChatGPT/create-expenseRequest-image-file";

            var requestImage = new
            {
                Base64Image = "",
                Extension = "",
                ApiKey = _apiKey,
                Url = url
            };

            var response = await _responseGPTService.GetResponseGPT(ocrApiUrl, requestImage, await _userManager.GetUserAsync(User));

            return Ok(response);
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
                throw new UnprocessableEntityException("Erro ao alterar o status da despesa.");

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
                throw new NotFoundException("Não existem despesas ativas.");

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
                throw new NotFoundException("Não existem despesas submetidas.");

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

        /// <summary>
        /// Delete an expense only soft mode.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Finance")]
        [HttpDelete("delete-soft")]
        public async Task<IActionResult> DeleteExpenseSoft(int id)
        {
            var result = await _expenseCRUDService.DeleteAsync(id, false);
            return Ok(result);
        }

    }
}
