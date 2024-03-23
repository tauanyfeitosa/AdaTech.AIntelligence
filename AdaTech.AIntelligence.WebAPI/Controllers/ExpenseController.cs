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
        private readonly IConfiguration _configuration;
        private readonly IExpenseCRUDService _expenseCRUDService;
        private readonly HttpClient _httpClient;
        private readonly ResponseGPTService _responseGPTService;
        private readonly string _apiKey;
        private readonly string _path;
        private readonly string _ocrApiUrl;
        private readonly UserManager<UserInfo> _userManager;

        public ExpenseController(IConfiguration configuration, IExpenseCRUDService expenseCRUDService, 
            ResponseGPTService responseGPTService, UserManager<UserInfo> userManager)
        {
            _configuration = configuration;
            _expenseCRUDService = expenseCRUDService;
            _userManager = userManager;
            _apiKey = _configuration.GetValue<string>("ApiKey")!;
            _path = _configuration.GetValue<string>("BaseOCRUrl")!;
            _ocrApiUrl = $"{_path}api/OCRChatGPT/create-expenseRequest-image-file";
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
            var (extension, base64Image) = await image.DescriptionImage();

            var requestImage = new
            {
                Base64Image = base64Image,
                Extension = extension,
                ApiKey = _apiKey,
                Url = ""
            };

            var response = await _responseGPTService.GetResponseGPT(_ocrApiUrl, requestImage, (await _userManager.GetUserAsync(User))!);

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
            var requestImage = new
            {
                Base64Image = "",
                Extension = "",
                ApiKey = _apiKey,
                Url = url
            };

            var response = await _responseGPTService.GetResponseGPT(_ocrApiUrl, requestImage, (await _userManager.GetUserAsync(User))!);

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
            var expense = await _expenseCRUDService.GetOne(idExpense) ?? throw new NotFoundException("Não existe despesa com este ID.");
            
            if (expense.Status == ExpenseStatus.PAID)
                throw new NotAnExpenseException("Despesa não encontrada.");

            expense.Status = ExpenseStatus.PAID;

            var success = await _expenseCRUDService.UpdateExpense(expense);

            if (!success)
                throw new UnprocessableEntityException("Erro ao alterar o status da despesa.");

            return Ok("Status da despesa atualizado com sucesso!");
        }

        /// <summary>
        /// View all expenses from authenticated user (actives and inactives)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("view-user-expenses")]
        [Authorize]
        public async Task<IActionResult> ViewUserExpenses()
        {
            var authenticatedUser = await _userManager.GetUserAsync(User) ?? throw new NotFoundException("Usuário com este ID não foi encontrado.");

            string userId = authenticatedUser.Id;

            var expenses = await _expenseCRUDService.GetUserExpenses(userId);

            if (expenses.IsNullOrEmpty())
                throw new NotFoundException("Não existem despesas.");

            // Selecionar apenas as propriedades desejadas das despesas
            var expenseInfos = expenses.Select(expense => new
            {
                expense.Id,
                expense.TotalValue,
                expense.Status,
                expense.Category,
                expense.Description,
                expense.IsActive,
                expense.CreatAt,
                expense.UpdateAt
            });

            return Ok(expenseInfos);
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
