using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class CartProducts : Entity
    {
        public String? UserId { get; set; }
        public User? User { get; set; }
        public IEnumerable<Product> Products { get; set; } = null!;
    }
}
