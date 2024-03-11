using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace AdaTech.AIntelligence.Service.Services
{
    public static class ImageService
    {
        public static async Task<string> GetImage(this IFormFile image, string extension)
        {
            if (image == null || image.Length == 0)
            {
                return "Upload a valid image file.";
            }

            var allowedExtensions = new[] { ".jpg", ".png" };


            if (!allowedExtensions.Contains(extension))
            {
                return "Only .jpg and .png file formats are allowed.";
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
    }
}
