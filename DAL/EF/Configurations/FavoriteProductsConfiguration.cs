using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class FavoriteProductsConfiguration : IEntityTypeConfiguration<FavoriteProduct>
    {
        public void Configure(EntityTypeBuilder<FavoriteProduct> builder)
        {
            builder.HasOne(x => x.User)
                   .WithOne(x => x.FavoriteProducts)
                   .HasForeignKey<FavoriteProduct>(fp => fp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
