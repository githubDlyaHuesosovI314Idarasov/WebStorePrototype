using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Order : Entity
    {
        public Guid UserId { get; set; }
        public Int64 OrderNumber { get; set; }
        public Int64 TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public User User { get; set; } = null!;
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
