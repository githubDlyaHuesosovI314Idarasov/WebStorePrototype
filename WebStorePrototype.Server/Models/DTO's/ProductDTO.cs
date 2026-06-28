using WebStorePrototype.Server.Models.Base;

namespace WebStorePrototype.Server.Models.DTO_s
{
    public class ProductDTO
    {
        public String Name { get; set; } = null!;
        public String Brand { get; set; } = null!;
        public String SKU { get; set; } = null!;
        public String SubcategoryName { get; set; } = null!;
        public Int64 Price { get; set; }
        public Int64 AverageRating { get; set; }
        public Int32 ReviewCount { get; set; }
        public Int64? DiscountedPrice { get; set; }
        public Boolean IsInStock { get; set; }
        public String? ThumbnailUrl { get; set; }

    }
}
