using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class CompareProductsConfiguration : IEntityTypeConfiguration<ComparedProducts>
    {
        public void Configure(EntityTypeBuilder<ComparedProducts> builder)
        {
            builder.HasOne(x => x.User)
                   .WithOne(x => x.ComparedProducts)
                   .HasForeignKey<ComparedProducts>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
