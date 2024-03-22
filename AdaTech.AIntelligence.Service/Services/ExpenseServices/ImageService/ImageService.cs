using Microsoft.AspNetCore.Http;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.ImageService
{
    /// <summary>
    /// Class to handle image processing.
    /// </summary>
    public static class ImageService
    {
        /// <summary>
        /// Converts the image to a base64 string.
        /// </summary>
        /// <param name="image">The image file to convert.</param>
        /// <param name="extension">The extension of the image file.</param>
        /// <returns>The base64 representation of the image.</returns>
        /// <exception cref="Exception">Thrown when the image format is not supported.</exception>
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
        /// Generates the description of the image including its URL and base64 representation.
        /// </summary>
        /// <param name="image">The image file to describe.</param>
        /// <returns>A tuple containing the extension and base64 representation of the image.</returns>
        public static async Task<(string extension, string base64)> DescriptionImage(this IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            var base64Image = await image.GetImageBase64(extension);

            return (extension, base64Image);
        }
    }
}
