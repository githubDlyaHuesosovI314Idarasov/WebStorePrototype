using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class FavoriteProduct : Entity
    {
        public String? UserId { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }  

    }
}
