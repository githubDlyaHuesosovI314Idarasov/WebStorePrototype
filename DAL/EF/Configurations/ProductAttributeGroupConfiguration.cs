using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class ProductAttributeGroupConfiguration : IEntityTypeConfiguration<ProductAttibuteGroup>
    {
        public void Configure(EntityTypeBuilder<ProductAttibuteGroup> builder)
        {
            builder.HasMany(x => x.ProductAttributes).WithOne(x => x.ProductAttibuteGroup).HasForeignKey(x => x.ProductAttibuteGroupId);
            builder.HasOne(x => x.Subcategory).WithMany(x => x.ProductAttibuteGroups).HasForeignKey(x => x.SubcategoryId);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductAttibuteGroups).HasForeignKey(x => x.ProductId);
        }
    }
}
