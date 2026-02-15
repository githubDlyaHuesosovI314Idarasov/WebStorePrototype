using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Category : Entity
    {
        public String Name { get; set; } = null!;
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
