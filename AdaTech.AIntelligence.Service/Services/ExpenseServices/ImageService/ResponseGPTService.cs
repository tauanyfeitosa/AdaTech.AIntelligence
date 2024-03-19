using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Entities.Enums;
using System.Net.Http.Json;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.ImageService
{
    /// <summary>
    /// Service class to handle responses from GPT-4.
    /// </summary>
    public class ResponseGPTService
    {
        private readonly IExpenseCRUDService _expenseCRUDService;
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseGPTService"/> class.
        /// </summary>
        /// <param name="expenseCRUDService">The expense CRUD service.</param>
        /// <param name="clientFactory">The HTTP client factory.</param>
        public ResponseGPTService(IExpenseCRUDService expenseCRUDService, IHttpClientFactory clientFactory)
        {
            _expenseCRUDService = expenseCRUDService;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Gets the response from GPT-4 and handles it accordingly.
        /// </summary>
        /// <param name="link">The API endpoint for GPT-4.</param>
        /// <param name="request">The request object to send to GPT-4.</param>
        /// <param name="user">The user information associated with the expense.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        public async Task<string> GetResponseGPT(string link, object request, UserInfo user)
        {
            var jsonContent = JsonContent.Create(request);
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsync(link, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                if (responseData.Contains("ERROR_RESPONSE"))

                    throw new NotAnExpenseException("Comprovante Inválido");

                var success = await CreateExpense(responseData, user);

                if (!success)
                    
                    throw new UnprocessableEntityException("Erro ao cadastrar despesa");

                return "Despesa criada com sucesso!";
            }

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Creates an expense based on the GPT-4 response.
        /// </summary>
        /// <param name="responseData">The response data from GPT-4.</param>
        /// <param name="user">The user information associated with the expense.</param>
        /// <returns>A boolean indicating whether the operation was successful.</returns>
        private async Task<bool> CreateExpense(string responseData, UserInfo user)
        {
            string[] values = responseData.Split(",");
            var responseObj = new Expense()
            {
                Category = (Category)int.Parse(values[0]),
                TotalValue = double.Parse(values[1].Replace(".", ",")),
                Description = values[2],
                Status = ExpenseStatus.Submitted,
                IsActive = true,
                UserInfo = user,
                UserInfoId = user.Id
            };

            var success = await _expenseCRUDService.CreateExpense(responseObj);
            return success;
        }
    }
}
