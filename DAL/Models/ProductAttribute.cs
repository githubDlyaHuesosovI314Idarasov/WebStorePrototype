using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ProductAttribute : Entity
    {
        public String Name { get; set; } = null!;
        public String Value { get; set; } = null!;
        public Guid ProductAttibuteGroupId { get; set; }
        public ProductAttibuteGroup ProductAttibuteGroup { get; set; } = null!;


    }
}
