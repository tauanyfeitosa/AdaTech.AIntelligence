﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.EntityFrameworkCore;


namespace AdaTech.AIntelligence.DbLibrary.EntityFramework.Configuration
{
    /// <summary>
    /// Configuration for the User entity based in IdentityUser.
    /// </summary>
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
            builder.Property(u => u.IsStaff).IsRequired();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
        }
    }
}
