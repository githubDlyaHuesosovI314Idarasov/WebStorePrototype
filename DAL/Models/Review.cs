using System;
using System.Collections.Generic;
using System.Text;


namespace DAL.Models
{
    public class Review : Entity
    {
        public Double Rating { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Product Product { get; set; } = null!;
        public Comment UserComment { get; set; } = null!;
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
