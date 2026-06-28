namespace WebStorePrototype.Server.Models.DTO_s
{
    public class ReviewDTO 
    {
        public Double Rating { get; set; }
        public String Comment { get; set; } = null!;
        public String UserId { get; set; } = null!;
        public Guid ProductId { get; set; }
        public String ProductName { get; set; } = null!;
    }
}
