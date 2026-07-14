namespace CarOrderApi.Dtos.Admin
{
    public class AdminOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ShippingAddress { get; set; }

        public string CarName { get; set; }

        public string CarImage { get; set; }

        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
