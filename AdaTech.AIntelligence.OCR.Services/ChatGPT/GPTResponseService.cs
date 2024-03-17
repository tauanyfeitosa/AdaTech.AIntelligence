using System.Net.Http.Headers;

namespace AdaTech.AIntelligence.OCR.Services.ChatGPT
{
    /// <summary>
    /// Class to handle the response from the chat GPT.
    /// </summary>
    public class GPTResponseService
    {
        /// <summary>
        /// Method to execute a request to the chat GPT.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="contentRequest"></param>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <returns cref="bool, string"> Returns a boolean that indicates if operation was successfull and the response content.</returns>
        public async Task<(bool success, string responseContent)> ExecuteRequest(string apiKey, StringContent contentRequest, HttpClient httpClient, string url)
        {
            SetAuthorizationHeader(apiKey, httpClient);

            var response = await httpClient.PostAsync(url, contentRequest);

            var responseContent = await response.ProcessResponse();

            return (response.IsSuccessStatusCode, responseContent);
        }

        /// <summary>
        /// Method to set the authorization header for the chat GPT request.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="httpClient"></param>
        public void SetAuthorizationHeader(string apiKey, HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }
    }
}
