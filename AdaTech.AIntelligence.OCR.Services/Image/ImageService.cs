
using Microsoft.AspNetCore.Http;

namespace AdaTech.AIntelligence.OCR.Services.Image
{
    /// <summary>
    /// Class to handle the image request.
    /// </summary>
    public static class ImageService
    {
        /// <summary>
        /// Method to validate the input image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="extension"></param>
        /// <returns cref="object">Returns a portion of the request that describes the image file.</returns>
        public static object DescriptionImage(this string image, string extension)
        {

            var urlImage = new
            {
                role = "user",
                content = new object[]
                        {
                            new { type = "text", text = "What’s in this image?" },
                            new { type = "image_url", image_url = $"data:image/{extension[1..]};base64,{image}" }
                        }
            };

            return urlImage;
        }

        /// <summary>
        /// Method to validate the input url
        /// </summary>
        /// <param name="url"></param>
        /// <returns cref="object">Returns a portion of the request that describes the url image.</returns>
        public static object DescriptionImage(this string url)
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
                                    url = $"{url}"
                                }
                            }
                        }
            };

            return urlObject;
        }

        public static async Task<object> DescriptionImage(this IFormFile image, string prompt)
        {
            (string base64, string extension) = await ConvertImage(image);

            var urlImage = new
            {
                role = "user",
                content = new object[]
                       {
                            new { type = "text", text = $"{prompt}" },
                            new { type = "image_url", image_url = $"data:image/{extension[1..]};base64,{base64}" }
                       }
            };
            return urlImage;

        }

        private static async Task<(string base64, string extension)> ConvertImage(IFormFile image)
        {

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            string base64Image;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }

            return (base64Image, extension);
        }


    }
}
