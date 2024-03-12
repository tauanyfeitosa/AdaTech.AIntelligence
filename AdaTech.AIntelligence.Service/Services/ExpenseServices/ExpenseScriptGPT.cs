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
                            new { type = "text", text = "Responder em formato CSV sem cabeçalho" },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "categoria da despesa entre: hospedagem = 1, transporte = 2, viagem = 3, alimentação = 4 ou Outros = 5. Se a imagem contiver somente itens comestíveis, a categoria deverá ser alimentação, se for misto, a categoria é Outros" },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "valor total da despesa utilizando . ao inves de ," },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "elabore a descricao da despesa em no máximo 30 caracteres, tente aproveitar o maximo de caracteres possivel, se houver somente 1 item na nota, descreva o item" },
                        }
                    },

                    url
                },
                max_tokens = 50,
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
