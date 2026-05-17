using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Order : Entity
    {
        public String UserId { get; set; } = null!;
        public Int64 OrderNumber { get; set; }
        public Int64 TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
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
