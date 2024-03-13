using System.Net.Http.Headers;
using System.Text.Json;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.ChatGPTServices
{
    public static class ChatGPTService
    {
        public static async Task<string> GenerateResponse(this string apiKey, IHttpClientFactory clientFactory)
        {
            using var httpClient = clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            HttpResponseMessage response;
            
            response = await httpClient.GetAsync("https://api.openai.com/v1/engines");
            
            if (response.IsSuccessStatusCode)
            {
                return "Conexão com o chat GPT bem-sucedida!";
            }

            throw new Exception("Erro ao conectar-se ao chat GPT.");
        }

        public static async Task<string> ProcessResponse(this HttpResponseMessage response)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return ParseResponseContent(jsonResponse);
        }

        private static string ParseResponseContent(string jsonResponse)
        {
            using var doc = JsonDocument.Parse(jsonResponse);
            var root = doc.RootElement;

            if (!root.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
            {
                return "No choices found in the response.";
            }

            var firstChoice = choices[0];
            if (!firstChoice.TryGetProperty("message", out var message) || !message.TryGetProperty("content", out var content))
            {
                return "Content property not found in the first choice.";
            }

            return content.GetString() ?? "Content is null.";
        }
    }
}
