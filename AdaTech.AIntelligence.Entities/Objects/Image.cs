using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class Image
    {
        public int Id { get; set; }
        public byte[]? ByteImage { get; set; }
        public string? URLImage { get; set; }
        public ImageSourceType SourceType { get; set; }
        public ImageProcessingStatus ProcessingStatus { get; set; }
        public Expense? Expense { get; set; }
        public int? ExpenseId { get; set; }
    }
}
