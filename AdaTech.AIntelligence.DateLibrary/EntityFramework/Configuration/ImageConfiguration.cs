using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaTech.AIntelligence.Entities.Enums;

namespace AdaTech.AIntelligence.DateLibrary.EntityFramework.Configuration
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(i => i.SourceType).IsRequired();
            builder.Property(i => i.ProcessingStatus).IsRequired();
            builder.Property(i => i.ByteImage);
            builder.Property(i => i.URLImage);
        }
    }
}