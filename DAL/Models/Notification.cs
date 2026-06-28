using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Notification 
    {
        public String Title { get; set; } = null!;
        public String ThumbnailUrl { get; set; } = null!;
        public String Text { get; set; } = null!;
    }
}
