using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.Data.Core.Configurations
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("author", "dbo")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
