using AdaTech.AIntelligence.OCR.Services.ChatGPT;
using AdaTech.AIntelligence.OCR.Services.Image;
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

        private const string _url = "https://api.openai.com/v1/chat/completions";

        public OCRChatGPTController(IHttpClientFactory clientFactory, ScriptGPTService scriptGPTService, 
            HttpClient httpClient, InputService inputService, GPTResponseService gptResponseService)
        {
            _clientFactory = clientFactory;
            _scriptGPTService = scriptGPTService;
            _httpClient = httpClient;
            _inputService = inputService;
            _gptResponseService = gptResponseService;
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
            var listImage = await CreateObjects();
            var contentRequest = _scriptGPTService.ScriptPrompt(urlObject,  listImage[0], listImage[1], listImage[2], listImage[3], listImage[4], listImage[5]);

            var (success, resposta) = await _gptResponseService.ExecuteRequest(requestImage.ApiKey, contentRequest, _httpClient, _url);

            if (!success)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }

        private async Task<object> ConvertImageObject(string prompt, string path)
        {

            var expenseImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), path).Replace("WebAPI", "Services");
            var image = await CreateIFormFileFromPath(expenseImageDirectory);
            var imageOject = await image.DescriptionImage(prompt);

            return imageOject;

        }

        private async Task<IFormFile> CreateIFormFileFromPath(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var memoryStream = new MemoryStream();
            using (var fileStream = fileInfo.OpenRead())
            {
                await fileStream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            var formFile = new FormFile(memoryStream, 0, memoryStream.Length, fileInfo.Name, fileInfo.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            return formFile;
        }

        private async Task<List<object>> CreateObjects()
        {
            var listObject =  new List<object>
            {
             await ConvertImageObject("Este cupom fiscal é uma nota fiscal e seu valor é 150,00 reais", "ExpenseImage\\Cupom.png"),
             await ConvertImageObject("Este cupom fiscal é uma nota fiscal e seu valor é 6,00 reais", "ExpenseImage\\Cupom2.jpeg"),
             await ConvertImageObject("Este NF é uma nota fiscal e seu valor é 400,00 reais", "ExpenseImage\\NF.jpg"),
             await ConvertImageObject("Este NF é uma nota fiscal e seu valor é 169,76 reais", "ExpenseImage\\NF2.jpeg"),
             await ConvertImageObject("Este Danfe é uma nota fiscal e seu valor é 19,90 reais", "ExpenseImage\\Danfe.png"),
             await ConvertImageObject("Este NF é uma nota fiscal e seu valor é 333,33 reais", "ExpenseImage\\NFRuim.jpeg"),
            };

            return listObject;
        }
    }
}
