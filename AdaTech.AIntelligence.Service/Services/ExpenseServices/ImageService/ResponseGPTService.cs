using AdaTech.AIntelligence.Entities.Enums;
using AdaTech.AIntelligence.Entities.Objects;
using AdaTech.AIntelligence.Service.Exceptions;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;
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

        public ResponseGPTService(IConfiguration configuration,
            IExpenseCRUDService expenseCRUDService, IHttpClientFactory httpClientFactory)
        {
            _expenseCRUDService = expenseCRUDService;
            _clientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get the response from GPT-4
        /// </summary>
        /// <param name="link"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetResponseGPT(string link, object request)
        {
            var jsonContent = JsonContent.Create(request);

            var client = _clientFactory.CreateClient();

            var response = await client.PostAsync(link, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                if (responseData.Contains("ERROR_RESPONSE"))
                    throw new NotAnExpenseException("Comprovante Inválido");
//                    return responseData;

                var success = await CreateExpense(responseData);

                if (!success)
                    return "Erro ao criar despesa.";

                return "Despesa cadastrada com sucesso!";
            }

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Create an expense from the response
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private async Task<bool> CreateExpense(string responseData)
        {
            string[] valores = responseData.Split(",");

            var respostaObjeto = new Expense()
            {
                Category = (Category)int.Parse(valores[0]),
                TotalValue = double.Parse(valores[1].Replace(".", ",")),
                Description = valores[2],
                Status = ExpenseStatus.SUBMITTED,
                IsActive = true
            };

            var success = await _expenseCRUDService.CreateExpense(respostaObjeto);

            return success;
        }
    }
}
