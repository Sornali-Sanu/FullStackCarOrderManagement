namespace CarOrderApi.Model
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CarId { get; set; }
        public ApplicationUser User { get; set; }
        public Car Car { get; set; }
    }
}
