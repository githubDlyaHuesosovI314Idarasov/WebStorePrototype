using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.Subcategory).WithMany(x => x.Products).HasForeignKey(x => x.SubcategoryId);
            builder.HasMany(x => x.Stocks).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.Images).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x=> x.Reviews).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ViewedProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.FavoriteProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.CartProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ComparedProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);



        }
    }
}
