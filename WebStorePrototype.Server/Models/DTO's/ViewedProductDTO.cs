using DAL;

namespace WebStorePrototype.Server.Models.DTO_s
{
    public class ViewedProductDTO : Entity
    {
        public Guid ProductId { get; set; } 
        public String ProductName { get; set; } = null!;
        public Int64 Price { get; set; }
        public Int64? DiscountedPrice { get; set; }
        public String ThumbnailUrl { get; set; } = null!;
        public Boolean IsInStock { get; set; }
            
    }
}
