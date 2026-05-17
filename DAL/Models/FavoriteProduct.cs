using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class FavoriteProduct : Entity
    {
        public String? UserId { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }  
    }
}
