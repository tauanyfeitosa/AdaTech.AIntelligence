using AdaTech.AIntelligence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdaTech.AIntelligence.DateLibrary.EntityFramework.Configuration
{
    internal class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(u => u.DateBirth).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired(); // pode não ser obrigatório
            builder.Property(u => u.CPF).IsRequired(); 
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.LastName).IsRequired(); // max /min length...
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsLogged).IsRequired();
            builder.Property(u => u.Login).IsRequired();
            builder.Property(u => u.Password).IsRequired();
        }
    }
}
