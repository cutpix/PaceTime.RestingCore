using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaceTime.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaceTime.Data.Core.Configurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("review", "dbo")
                   .HasKey(x => x.Id);
        }
    }
}
