using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class Expense
    {
        public int Id { get; set; }
        public double TotalValue { get; set; }
        public ExpenseStatus Status { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public Image Image { get; set; }
        public int ImageId { get; set; }
    }
}
