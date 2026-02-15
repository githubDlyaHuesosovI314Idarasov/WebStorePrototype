using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public sealed class Stock : Entity
    {
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        [ForeignKey("LocationId")]
        public Guid LocationId { get; set; }
        public Product Product { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public Int32 Quantity { get; set; }
    }
}
