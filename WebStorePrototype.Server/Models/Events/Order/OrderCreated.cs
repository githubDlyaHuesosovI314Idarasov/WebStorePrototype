using DAL.Models;

namespace WebStorePrototype.Server.Models.Events.Order
{
    public class OrderCreated
    {
        public Guid Id { get; set; }
        public String UserId { get; set; } = null!;
        public String OrderNumber { get; set; } = null!;
        public Int64 TotalAmount { get; set; }
        public DateTime WhenCreated { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public IEnumerable<Product> Products { get; set; } = null!;
    }
}
