namespace CarOrderApi.Dtos
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ProfileImage { get; set; } = null!;
       
    }
}
