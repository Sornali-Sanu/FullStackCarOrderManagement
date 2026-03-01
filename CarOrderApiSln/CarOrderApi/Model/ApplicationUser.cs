using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace CarOrderApi.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        //nullable dita hoba.
        public string ProfileImageUrl { get; set; }

        //address:
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        //verification
        public string DrivingLicenseNumber { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }

        //Navigation:
        public ICollection<Order> Orders { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
