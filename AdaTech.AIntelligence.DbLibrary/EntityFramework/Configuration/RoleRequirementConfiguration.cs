using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdaTech.AIntelligence.DbLibrary.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the RoleRequirement entity
    /// </summary>
    internal class RoleRequirementConfiguration : IEntityTypeConfiguration<RoleRequirement>
    {
        public void Configure(EntityTypeBuilder<RoleRequirement> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(r => r.Role).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.UserInfoId).IsRequired();
            builder.Property(r => r.RequestDate).IsRequired();

            builder.HasOne(r => r.UserInfo)
                .WithMany(u => u.RoleRequirements)
                .HasForeignKey(r => r.UserInfoId);
        }
    }
}
