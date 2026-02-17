namespace CarOrderApi.Model
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Gearbox { get; set; }
        public decimal Engine { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public Condition Condition { get; set; }
        public bool AirCon { get; set; }
        public Drivetype DriveType { get; set; }

    }
}
