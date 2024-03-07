using AdaTech.AIntelligence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdaTech.AIntelligence.DateLibrary.EntityFramework.Configuration
{
    internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.Description).IsRequired();
        }
    }
}
