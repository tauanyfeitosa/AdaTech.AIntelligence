using AdaTech.AIntelligence.Exceptions.ErrosExceptions.ExceptionsCustomer;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AdaTech.AIntelligence.OCR.Services.ChatGPT
{
    /// <summary>
    /// Class to handle the chat GPT service.
    /// </summary>
    public static class ChatGPTService
    {
        /// <summary>
        /// Extension method to generate a response from the chat GPT.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="clientFactory"></param>
        /// <returns cref="string">Returns a string indicating if connection with chat GPT 4 API was successfull.</returns>
        /// <exception cref="Exception"></exception>
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

            throw new NotConnectionGPTException("Erro ao conectar-se ao chat GPT.");
        }

        /// <summary>
        /// Extension method to process the response from the chat GPT.
        /// </summary>
        /// <param name="response"></param>
        /// <returns cref="string">Returns parse response content in string format.</returns>
        public static async Task<string> ProcessResponse(this HttpResponseMessage response)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return ParseResponseContent(jsonResponse);
        }

        /// <summary>
        /// Method to parse the response content from the chat GPT.
        /// </summary>
        /// <param name="jsonResponse"></param>
        /// <returns cref="string">Returns JSON response content as a string.</returns>
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
