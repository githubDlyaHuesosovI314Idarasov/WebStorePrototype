using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasMany(x => x.Stocks).WithOne(x => x.Location).HasForeignKey(x => x.LocationId);
            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(p => p.Street).HasMaxLength(100);
                a.Property(p => p.City).HasMaxLength(50);
                a.Property(p => p.HouseNumber).HasMaxLength(10);
                a.Property(p => p.PostalCode).HasMaxLength(50);
            });
        }
    }
}
