using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices
{
    public static class ExpenseResponseGPTService
    {
        public static async Task<string> ProcessResponse(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
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

            var contentString = content.GetString();
            Console.WriteLine(contentString);
            return contentString;
        }
    }
}
