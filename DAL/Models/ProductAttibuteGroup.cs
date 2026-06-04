using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ProductAttibuteGroup : Entity
    {
        public String GroupName { get; set; } = null!;
        public Guid SubcategoryId { get; set; }
        public Guid ProductId { get; set; }
        public Subcategory Subcategory { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public IEnumerable<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    }
}
