
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
        public static async Task<object> DescriptionImage(this string image, string extension)
        {

            var urlImage = new
            {
                role = "user",
                content = new object[]
                        {
                            new { type = "text", text = "What’s in this image?" },
                            new { type = "image_url", image_url = $"data:image/{extension.Substring(1)};base64,{image}" }
                        }
            };

            return urlImage;
        }

        /// <summary>
        /// Method to validate the input url
        /// </summary>
        /// <param name="url"></param>
        /// <returns cref="object">Returns a portion of the request that describes the url image.</returns>
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
                                    url = $"{url}"
                                }
                            }
                        }
            };

            return urlObject;
        }
    }
}
