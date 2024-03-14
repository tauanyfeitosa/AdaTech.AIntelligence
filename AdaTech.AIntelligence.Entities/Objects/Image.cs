using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class Image
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ImageSourceType SourceType { get; set; }
        public ImageProcessingStatus ProcessingStatus { get; set; }
        public Expense? Expense { get; set; }
        public int? ExpenseId { get; set; }
    }
}
