using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ComparedProduct : Entity
    {
        public String? UserId { get; set; }
        public Guid ProductId { get; set; }
        public Subcategory Subcategory { get; set; } = null!;
        public Product? Product { get; set; } = null!;       
    }
}
