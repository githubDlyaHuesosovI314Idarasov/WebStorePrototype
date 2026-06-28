using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class UserNotifications : Entity
    {
        public String UserId { get; set; } = null!;
        public IEnumerable<Notification> Notifications { get; } = new List<Notification>();
    }
}
