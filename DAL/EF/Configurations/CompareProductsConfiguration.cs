using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class ComparedProductConfiguration : IEntityTypeConfiguration<ComparedProduct>
    {
        public void Configure(EntityTypeBuilder<ComparedProduct> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.ComparedProducts).HasForeignKey(x => x.ProductId);
        }
    }
}
