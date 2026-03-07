using System.ComponentModel.DataAnnotations;

namespace CarOrderApi.Dtos
{
    public class UpdateProfileDto
    {
       
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }

        public string StreetAddress { get; set; }

        public string PostalCode { get; set; }

        public string DrivingLicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        public string? ProfileImageUrl { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
