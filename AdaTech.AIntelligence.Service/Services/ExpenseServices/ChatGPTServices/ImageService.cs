using Microsoft.AspNetCore.Http;

namespace AdaTech.AIntelligence.Service.Services.ExpenseServices.ChatGPTServices
{
    public static class ImageService
    {
        private static async Task<string> GetImageBase64(this IFormFile image, string extension)
        {
            if (image == null || image.Length == 0)
            {
                return "Upload a valid image file.";
            }

            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };


            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Somente são aceitas imagens nos formatos JPG, JPEG e PNG.");
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

        public static async Task<(object url, string base64)> DescriptionImage(this IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            var base64Image = await image.GetImageBase64(extension);

            var urlImage = new
            {
                role = "user",
                content = new object[]
                        {
                            new { type = "text", text = "What’s in this image?" },
                            new { type = "image_url", image_url = $"data:image/{extension.Substring(1)};base64,{base64Image}" }
                        }
            };

            return (urlImage, base64Image);
        }

        public static async Task<object> DescriptionImage(this string url)
        {
            var urlObject = new
            {
                role = "user",
                content = new object[]
                        {
                            new
                            {
                                type = "image_url", image_url = new
                                {
                                    url = $"{url}",
                                    detail="low"
                                }
                            }
                        }
            };

            return urlObject;
        }
    }
}
