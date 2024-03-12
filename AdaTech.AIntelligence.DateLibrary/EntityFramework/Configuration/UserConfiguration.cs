using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdaTech.AIntelligence.DateLibrary.EntityFramework.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.Property(u => u.DateBirth).IsRequired();
            builder.Property(u => u.PhoneNumber);
            builder.Property(u => u.CPF).IsRequired(); 
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.LastName).IsRequired(); 
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsLogged).IsRequired();
            builder.Property(u => u.IsStaff).IsRequired();
            builder.Property(u => u.IsSuperUser).IsRequired();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PromoteStatus);
        }
    }
}
