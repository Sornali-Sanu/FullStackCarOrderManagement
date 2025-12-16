namespace CarOrderApi.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

    }
}
