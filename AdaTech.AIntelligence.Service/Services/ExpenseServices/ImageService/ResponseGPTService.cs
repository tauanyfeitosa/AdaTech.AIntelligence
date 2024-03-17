using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.ImageService
{
    /// <summary>
    /// Class to handle the response from GPT-4
    /// </summary>
    public class ResponseGPTService
    {
        private readonly IExpenseCRUDService _expenseCRUDService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<UserInfo> _userManager;

        public ResponseGPTService(IExpenseCRUDService expenseCRUDService, IHttpClientFactory clientFactory, UserManager<UserInfo> userManager)
        {
            _expenseCRUDService = expenseCRUDService;
            _clientFactory = clientFactory;
            _userManager = userManager;
        }

        /// <summary>
        /// Get the response from GPT-4
        /// </summary>
        /// <param name="link"></param>
        /// <param name="request"></param>
        /// <returns></returns>
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

                return "Despesa cadastrada com sucesso!";
            }

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Create an expense from the response
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private async Task<bool> CreateExpense(string responseData, UserInfo user)
        {
            string[] valores = responseData.Split(",");

            var respostaObjeto = new Expense()
            {
                Category = (Category)int.Parse(valores[0]),
                TotalValue = double.Parse(valores[1].Replace(".", ",")),
                Description = valores[2],
                Status = ExpenseStatus.SUBMITTED,
                IsActive = true,
                UserInfo = user, 
                UserInfoId = user.Id
            };

            var success = await _expenseCRUDService.CreateExpense(respostaObjeto);

            return success;
        }
    }
}
