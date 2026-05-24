using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.CartProducts).HasForeignKey(x => x.ProductId);
        }
    }
}
