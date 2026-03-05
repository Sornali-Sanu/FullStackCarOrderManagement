using CarOrderApi.Data;
using CarOrderApi.Dtos;
using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public UserRepository(AppDbContext context,UserManager<ApplicationUser>userManager,IWebHostEnvironment env)
        {
            _context=context;
            _userManager=userManager;
            _env=env;
        }
        public async Task AddToWishlistAsync(Wishlist wishlist)
        {
          await _context.Wishlists.AddAsync(wishlist);
          await _context.SaveChangesAsync();
        }

        public async Task<ProfileResponseDto> GetUserByIdAsync(string userId)
        {
            var user= await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return new ProfileResponseDto
            {
                UserName=user.UserName,
                Email=user.Email,
                FullName=user.FullName,
                PhoneNumber = user.PhoneNumber,
                Country = user.Country,
                City = user.City,
                StreetAddress = user.StreetAddress,
                PostalCode = user.PostalCode,
                DrivingLicenseNumber = user.DrivingLicenseNumber,
                LicenseExpiryDate = user.LicenseExpiryDate,
                ProfileImageUrl = user.ProfileImageUrl
            };
        }

        public async Task<List<Order>> GetUserOrderAsync(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).Include(x => x.Car).ToListAsync();
        }

        public async Task<List<Wishlist>> GetUserWishlistAsync(string userId)
        {
            return await _context.Wishlists.Where(x => x.UserId == userId).Include(x => x.Car).ToListAsync();
        }

        public async Task<ApplicationUser> UpdateUserAsync(UpdateProfileDto user,string userId)
        {
            var userProfile = await _context.ApplicationUsers.FirstOrDefaultAsync(x=>x.Id==userId);
            if (userProfile == null)
            { return null; }

            userProfile.FullName = user.FullName;
            userProfile.LicenseExpiryDate=user.LicenseExpiryDate;
            userProfile.UserName=user.UserName;
            userProfile.PhoneNumber=user.PhoneNumber;
            userProfile.Country=user.Country;
            userProfile.City=user.City;
            userProfile.PostalCode=user.PostalCode;
            userProfile.DrivingLicenseNumber=user.DrivingLicenseNumber;
            //userProfile.ProfileImageUrl=user.ProfileImageUrl;

            await _context.SaveChangesAsync();
            return userProfile;
            
           
         
        }
    }
}
