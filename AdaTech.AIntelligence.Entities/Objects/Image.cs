using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class Image
    {
        public int Id { get; set; }
        public byte[]? ByteImage { get; set; }
        public string Path { get; set; }
        public ImageSourceType ImageSourceType { get; set; }
    }
}
