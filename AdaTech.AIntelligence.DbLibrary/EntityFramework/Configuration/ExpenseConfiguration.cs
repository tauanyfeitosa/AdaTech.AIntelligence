using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdaTech.AIntelligence.DbLibrary.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the Expense entity
    /// </summary>
    internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.CreatAt).IsRequired();
            builder.Property(e => e.UserInfoId).IsRequired();
            builder.Property(e => e.TotalValue).IsRequired();

            builder.HasOne(e => e.UserInfo)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserInfoId);
        }
    }
}
