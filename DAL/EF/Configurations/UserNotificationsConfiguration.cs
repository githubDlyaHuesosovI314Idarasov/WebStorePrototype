using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Configurations
{
    public class UserNotificationsConfiguration : IEntityTypeConfiguration<UserNotifications>
    {
        public void Configure(EntityTypeBuilder<UserNotifications> builder)
        {
            builder.OwnsMany(x => x.Notifications, p =>
            {
                p.Property(n => n.Title).HasMaxLength(100);
                p.Property(n => n.ThumbnailUrl);
                p.Property(n => n.Text).HasMaxLength(1024);
            });
        }
    }
}
