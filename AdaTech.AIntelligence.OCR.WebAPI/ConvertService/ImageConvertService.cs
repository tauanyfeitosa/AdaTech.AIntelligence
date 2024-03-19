using AdaTech.AIntelligence.OCR.Services.Image;

namespace AdaTech.AIntelligence.OCR.WebAPI.ConvertService
{
    /// <summary>
    /// Convert the image to a object to be used in the chatGPT service.
    /// </summary>
    public class ImageConvertService
    {
        /// <summary>
        /// Convert the image to a object to be used in the chatGPT service.
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<object> ConvertImageObject(string prompt, string path)
        {

            var expenseImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), path).Replace("WebAPI", "Services");
            var image = await CreateIFormFileFromPath(expenseImageDirectory);
            var imageOject = await image.DescriptionImage(prompt);

            return imageOject;

        }

        /// <summary>
        /// Create a IFormFile from a path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a list of objects to be used in the chatGPT service.
        /// </summary>
        /// <returns></returns>
        public async Task<List<object>> CreateObjects()
        {
            var listObject = new List<object>
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
