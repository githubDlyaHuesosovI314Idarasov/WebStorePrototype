using DAL.Models;

namespace Contracts
{
    public class OrderCreated
    {
        public Guid Id { get; set; }
        public String UserId { get; set; } = null!;
        public Int64 OrderNumber { get; set; }
        public Int64 TotalAmount { get; set; }
        public DateTime WhenCreated { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public IEnumerable<Product> Products { get; set; } = null!;
    }
}
