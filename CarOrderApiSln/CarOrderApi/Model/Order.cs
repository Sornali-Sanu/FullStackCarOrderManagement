namespace CarOrderApi.Model
{
    public class Order

    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CarId { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Panding";

        public ApplicationUser User { get; set; }
        public Car Car { get; set; }
    }
}
