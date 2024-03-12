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

            builder.OwnsOne(e => e.Image, image =>
            {
                image.Property(e => e.ByteImage).HasColumnName("ImageInBytes");
                image.Property(e => e.ImageSourceType).HasColumnName("ImageSourceType").IsRequired();
                image.Property(e => e.Path).HasColumnName("ImagePath").IsRequired();
            });
        }
    }
}
