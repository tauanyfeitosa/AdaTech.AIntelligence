using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.Service.Services
{
    public static class ChatGPTService
    {
        public static async Task<string> GenerateResponse(this string apiKey, IHttpClientFactory clientFactory)
        {
            using var httpClient = clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync("https://api.openai.com/v1/engines");
            }
            catch (HttpRequestException)
            {
                throw new Exception("Erro ao se conectar ao chat GPT.");
            }

            if (response.IsSuccessStatusCode)
            {
                return "Conexão com o chat GPT bem-sucedida!";
            }
            
            throw new Exception("Erro ao conectar-se ao chat GPT.");
        }
    }
}
