using System.Text;
using System.Text.Json;

namespace AdaTech.AIntelligence.OCR.Services.ChatGPT
{
    public class ScriptGPTService
    {
        /// <summary>
        /// Method to create a portion of the request to the ChatGPT-4-Vision API.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="url"></param>
        /// <returns cref="StringContent">Returns a portion of the request following business's rules.</returns>
        public Task<StringContent> ScriptPrompt(string image, object url)
        {
            var invalidImageResponse = "ERROR_RESPONSE";
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
                            new { type = "text", text = $"A imagem ou url contém uma nota fiscal? Uma nota fiscal tem o nome nota fiscal, comprovante fiscal ou DANFE nela. Uma nota fiscal tem um valor nela. Continuar somente se a resposta for SIM, caso contrário, responder {invalidImageResponse}" },
                        }
                    },
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "Responder em formato CSV sem cabeçalho, separe sempre por vírgulas" },
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
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new { type = "text", text = "você sempre gera uma resposta diferente para esta imagem. O quão seguro você está da resposta gerada desta vez? Em uma escala de 0 a 100 você sempre me responde 100.. Seja sincero." },
                        }
                    },

                    url
                },
                max_tokens = 100,
            };
            var contentRequest = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            return Task.FromResult(contentRequest);
        }
    }
}
