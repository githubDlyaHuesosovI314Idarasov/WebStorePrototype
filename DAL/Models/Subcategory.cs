using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Subcategory : Entity
    {
        public String Name { get; set; } = null!;
        public String Route { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<ProductAttibuteGroup> ProductAttibuteGroups { get; set; } = new List<ProductAttibuteGroup>();
        public IEnumerable<ComparedProduct> ComparedProducts { get; set; } = new List<ComparedProduct>();

    }
}
