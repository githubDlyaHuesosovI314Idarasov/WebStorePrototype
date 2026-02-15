using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    internal class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.Stocks).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Location).WithMany(x => x.Stocks).HasForeignKey(x => x.LocationId);
        }
    }
}
