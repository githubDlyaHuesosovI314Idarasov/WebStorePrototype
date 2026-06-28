using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Comment
    {
        public String UserId { get; set; } = null!;
        public String Text { get; set; } = null!;
    }
}
