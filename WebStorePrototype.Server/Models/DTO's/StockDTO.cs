namespace WebStorePrototype.Server.Models.DTO_s
{
    public class StockDTO
    {
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }
        public Int32 Quantity { get; set; }
    }
}
