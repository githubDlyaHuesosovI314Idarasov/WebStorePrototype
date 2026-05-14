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

    }
}
