namespace AdaTech.AIntelligence.OCR.Services.Image
{
    /// <summary>
    /// DTO class to handle the request image.
    /// </summary>
    public class RequestImage
    {
        public string? Base64Image { get; set; }
        public string? Extension { get; set; }
        public string ApiKey { get; set; }
        public string? Url { get; set; }
    }
}
