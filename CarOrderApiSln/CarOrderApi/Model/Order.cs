namespace CarOrderApi.Model
{
    public class Order
    {
        public int OrderId { get; set; }

        public int CarId { get; set; }
        public string CustomerId { get; set; }

        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public Car Car { get; set; }
    }
}
