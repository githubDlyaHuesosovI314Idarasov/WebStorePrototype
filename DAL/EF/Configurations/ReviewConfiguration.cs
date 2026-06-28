using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.OwnsOne(x => x.UserComment, p =>
            {
                p.Property(c => c.Text).HasMaxLength(1024);
                p.Property(c => c.UserId);
            });
            builder.OwnsMany(x => x.Comments, p =>
            {
                p.Property(c => c.Text).HasMaxLength(1024);
                p.Property(c => c.UserId);
            });
        }
    }
}
