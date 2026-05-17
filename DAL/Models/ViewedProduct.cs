using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ViewedProduct : Entity
    {
        public String? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime WhenViewed { get; set; }
        public Product Product { get; set; } = null!;
    }
}
