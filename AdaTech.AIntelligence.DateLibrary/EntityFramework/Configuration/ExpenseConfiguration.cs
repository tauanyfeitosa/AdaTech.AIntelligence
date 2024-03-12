using AdaTech.AIntelligence.Entities.Objects;
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
            builder.Property(e => e.IsActive).IsRequired();
            builder.HasOne(e => e.Image)
                   .WithOne()
                   .HasForeignKey<Expense>(e => e.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
