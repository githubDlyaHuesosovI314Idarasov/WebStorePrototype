using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class FavoriteProductsConfiguration : IEntityTypeConfiguration<FavoriteProducts>
    {
        public void Configure(EntityTypeBuilder<FavoriteProducts> builder)
        {
            builder.HasOne(x => x.User)
                   .WithOne(x => x.FavoriteProducts)
                   .HasForeignKey<FavoriteProducts>(fp => fp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
