using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Product : Entity
    {
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;
        public String Brand { get; set; } = null!;
        public String SKU { get; set; } = null!;
        public Int64 Price { get; set; }
        public Int64? DiscountedPrice { get; set; }
        public Boolean IsOnSale { get; set; }
        public Boolean IsCouponApplicable { get; set; }
        public Boolean IsInStock { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public IEnumerable<Stock> Stocks { get; set; } = new List<Stock>();
        public IEnumerable<ProductImage> Images { get; set; } = new List<ProductImage>();
        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
        public IEnumerable<ViewedProduct> ViewedProducts { get; set; } = new List<ViewedProduct>();
        public IEnumerable<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();
        public IEnumerable<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    }
}
