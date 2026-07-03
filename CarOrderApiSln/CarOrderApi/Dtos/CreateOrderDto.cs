namespace CarOrderApi.Dtos
{
    public class CreateOrderDto
    {
        public int CarId { get; set; }

        public int Quantity { get; set; }

        public string PhoneNumber { get; set; }

        public string ShippingAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public string? PaymentMethods { get; set; }
    }
}
