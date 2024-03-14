using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.Entities.Objects
{
    /// <summary>
    /// Class to represent the expense with properties to control the expense information
    /// </summary>
    public class Expense
    {
        public int Id { get; set; }
        public double TotalValue { get; set; }
        public ExpenseStatus Status { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
