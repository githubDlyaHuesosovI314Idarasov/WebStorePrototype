using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Category : Entity
    {
        public String Name { get; set; } = null!;
        public String Icon { get; set; } = null!;
        public String Route { get; set; } = null!;
        public IEnumerable<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
