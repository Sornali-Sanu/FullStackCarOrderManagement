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

        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public ApplicationUser User { get; set; }
        public Car Car { get; set; }
        public int Quantity { get; set; }
    }
}
