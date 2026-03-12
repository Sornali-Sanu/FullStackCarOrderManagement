namespace CarOrderApi.Dtos.UserDtos
{
    public class ProfileResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }

        //address:
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        //verification
        public string DrivingLicenseNumber { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
    }
}
