using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class ViewedProductConfiguration : IEntityTypeConfiguration<ViewedProduct>
    {
        public void Configure(EntityTypeBuilder<ViewedProduct> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.ViewedProducts).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
