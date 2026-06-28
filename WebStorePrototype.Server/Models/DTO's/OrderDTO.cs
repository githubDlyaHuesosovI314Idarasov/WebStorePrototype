using DAL.Models;

namespace WebStorePrototype.Server.Models.DTO_s
{
    public class OrderDTO
    {
        public Int64 TotalAmount { get; set; }
        public Int64 OrderNumber { get; set; }
        public Int64 Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public String UserId { get; set; } = null!;
        public IEnumerable<Guid> ProductIds { get; set; } = null!;


    }
}
