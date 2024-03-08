using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class Expense
    {
        public int Id { get; set; }
        public ExpenseStatus Status { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
    }
}
