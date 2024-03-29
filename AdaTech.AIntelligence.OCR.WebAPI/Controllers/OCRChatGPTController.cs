using AdaTech.AIntelligence.OCR.Services.ChatGPT;
using AdaTech.AIntelligence.OCR.Services.Image;
using AdaTech.AIntelligence.OCR.WebAPI.ConvertService;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.OCR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRChatGPTController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ScriptGPTService _scriptGPTService;
        private readonly HttpClient _httpClient;
        private readonly InputService _inputService;
        private readonly GPTResponseService _gptResponseService;
        private readonly ImageConvertService _imageConvertService;

        private const string _url = "https://api.openai.com/v1/chat/completions";

        public OCRChatGPTController(IHttpClientFactory clientFactory, ScriptGPTService scriptGPTService, 
            HttpClient httpClient, InputService inputService, GPTResponseService gptResponseService,
            ImageConvertService imageConvertService)
        {
            _clientFactory = clientFactory;
            _scriptGPTService = scriptGPTService;
            _httpClient = httpClient;
            _inputService = inputService;
            _gptResponseService = gptResponseService;
            _imageConvertService = imageConvertService;
        }

        /// <summary>
        /// Checks the connection to the Chat GPT 4 Vision API.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet("check")]
        public async Task<IActionResult> CheckGptConnection([FromQuery] string apiKey)
        {

            var response = await apiKey.GenerateResponse(_clientFactory);

            return Ok(response);
        }

        /// <summary>
        /// Create an expense from an image in format CSV
        /// </summary>
        /// <param name="requestImage"></param>
        /// <returns></returns>
        [HttpPost("create-expenseRequest-image-file")]
        public async Task<IActionResult> CreateExpenseImage([FromBody] RequestImage requestImage)
        {
            var image = new List<string?>
            {
                !string.IsNullOrEmpty(requestImage.Base64Image) ? requestImage.Base64Image : null,
                !string.IsNullOrEmpty(requestImage.Extension) ? requestImage.Extension : null
            }.Where(s => !string.IsNullOrEmpty(s)).ToList();

            if (!_inputService.ValidateInput(image!, requestImage.Url))
            {
                return BadRequest("Nenhuma imagem ou URL foi enviada ou foram enviados tanto imagem quanto URL.");
            }

            var (base64Image, urlObject) = _inputService.ProcessImageOrUrl(image!, requestImage.Url);

            var urlFinal = _inputService.DetermineFinalUrl(base64Image, requestImage.Url);
            var listImage = await _imageConvertService.CreateObjects();
            var contentRequest = _scriptGPTService.ScriptPrompt(urlObject,  listImage[0], listImage[1], listImage[2], listImage[3], listImage[4], listImage[5]);

            var (success, resposta) = await _gptResponseService.ExecuteRequest(requestImage.ApiKey, contentRequest, _httpClient, _url);

            if (!success)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }
    }
}
