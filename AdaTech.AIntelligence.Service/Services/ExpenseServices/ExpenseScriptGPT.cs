using System.Text;
using System.Text.Json;
using AdaTech.AIntelligence.Service.Services.ExpenseServices.IExpense;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    public class ExpenseScriptGPT : IExpenseScriptGPT
    {
        public Task<StringContent> ExpenseScriptPrompt(string imagem, object url)
        {
            var imagemInvalida = @"HTTP/1.1 400 Bad Request{" + "\"message\": \"Comprovante Inválido\"}";
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
                            new { type = "text", text = $"A imagem contém um comprovante fiscal? Continuar somente se a resposta for SIM, caso contrário, responder {imagemInvalida}" },
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

                    url
                },
                max_tokens = 300
            };
            var contentRequest = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            return Task.FromResult((contentRequest));
        }

        public static bool IsBase64String(string base64String)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
