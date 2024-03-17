using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.OCR.Services.Image
{
    /// <summary>
    /// Class to handle the input image or url.
    /// </summary>
    public class InputService
    {
        /// <summary>
        /// Method to validate the input image or url
        /// </summary>
        /// <param name="image"></param>
        /// <param name="url"></param>
        /// <returns cref="bool">Returns a boolean indicating if input image is valid.</returns>
        public bool ValidateInput(List<string> image, string? url)
        {
            return !(image.Count != 2 && string.IsNullOrEmpty(url)) && !(image.Count == 2 && !string.IsNullOrEmpty(url));
        }

        /// <summary>
        /// Method to process the input image or url
        /// </summary>
        /// <param name="image"></param>
        /// <param name="url"></param>
        /// <returns cref="(string, object)">Returns base64 and description of the image.</returns>
        public async Task<(string base64Image, object urlObject)> ProcessImageOrUrl(List<string> image, string? url)
        {
            var base64Image = string.Empty;
            object urlObject = new object();

            if (image.Count == 2)
            {
                urlObject = await image[0].DescriptionImage(image[1]);
                base64Image = image[0];
            }
            else if (!string.IsNullOrEmpty(url))
            {
                urlObject = await url.DescriptionImage();
            }

            return (base64Image, urlObject);
        }

        /// <summary>
        /// Method to determine the final url
        /// </summary>
        /// <param name="base64Image"></param>
        /// <param name="url"></param>
        /// <returns cref="string">Returns final url based on which one is not null or empty.</returns>
        public string DetermineFinalUrl(string base64Image, string? url)
        {
            return !string.IsNullOrEmpty(base64Image) ? base64Image : url;
        }
    }
}
