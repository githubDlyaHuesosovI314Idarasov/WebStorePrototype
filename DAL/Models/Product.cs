using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Product : Entity
    {
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public IEnumerable<Stock> Stocks { get; set; } = new List<Stock>();
        public IEnumerable<ProductImage> Images { get; set; } = new List<ProductImage>();

    }
}
