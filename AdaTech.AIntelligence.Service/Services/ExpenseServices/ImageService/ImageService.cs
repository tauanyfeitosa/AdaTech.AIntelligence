using Microsoft.AspNetCore.Http;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.ImageService
{
    /// <summary>
    /// Class to handle the image request.
    /// </summary>
    public static class ImageService
    {
        /// <summary>
        /// Method to convert the image to base64
        /// </summary>
        /// <param name="image"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task<string> GetImageBase64(this IFormFile image, string extension)
        {
            if (image == null || image.Length == 0)
            {
                return "Upload a valid image file.";
            }

            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };


            if (!allowedExtensions.Contains(extension))
            {
                throw new FormatException("Somente são aceitas imagens nos formatos JPG, JPEG e PNG.");
            }

            string base64Image;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }

            return base64Image;
        }

        /// <summary>
        /// Method to generate the description of the image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static async Task<(string url, string base64)> DescriptionImage(this IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            var base64Image = await image.GetImageBase64(extension);

            return (extension, base64Image);
        }
    }
}
