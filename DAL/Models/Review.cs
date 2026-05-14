using System;
using System.Collections.Generic;
using System.Text;


namespace DAL.Models
{
    public class Review : Entity
    {
        public Double Rating { get; set; }
        public String Comment { get; set; } = null!;
        public String UserId { get; set; } = null!;
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
        
    }
}
