using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ProductImage : Entity
    {
        public String Url { get; set; } = null!;
        public Guid ProductId { get; set; } 
        public Product Product { get; set; } = null!;

    }
}
