using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    public class DTOImageRegister
    {
        public byte[]? ByteImage { get; set; }
        public string? URLImage { get; set; }
        public ImageProcessingStatus ProcessingStatus { get; set; }
    }
}
