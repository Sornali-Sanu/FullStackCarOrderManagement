using CarOrderApi.Data;
using CarOrderApi.Dtos.UserDtos;
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
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context,UserManager<ApplicationUser>userManager,IWebHostEnvironment env,ILogger<UserRepository>logger)
        {
            _context=context;
            _userManager=userManager;
            _env=env;
            _logger=logger;
        }
       
    //GetUser:
        public async Task<ProfileResponseDto> GetUserByIdAsync(string userId)
        {   //log info:
            _logger.LogInformation($"Fetching profile for user {userId}");
            var user= await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with id {userId}");
                return null;
            }
            _logger.LogInformation($"Profile fetched successfully for user {userId}");
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

      //GetImage:

        private async Task<string?> GetUserProfileImageName(IFormFile ProfileImage)
        {
            string userFileName = null;
            if (ProfileImage != null) {
                //Guid.NewGuid create unique File Name
                userFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                //Path.Combine create folder location
                var uploadFolder = Path.Combine(_env.WebRootPath, "UserImages");
                var filePath = Path.Combine(uploadFolder, userFileName);
                //fileStream create an empty file in folder path and copy to async copy the data to the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(fileStream);
                }
              
                    }
            return userFileName;
        }


        public async Task<List<Order>> GetUserOrderAsync(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).Include(x => x.Car).ToListAsync();
        }
        //get wishlist:
        public async Task<List<Wishlist>> GetUserWishlistAsync(string userId)
        {
            return await _context.Wishlists.Where(x => x.UserId == userId).Include(x => x.Car).ToListAsync();
        }
        //Update User
        public async Task<ApplicationUser> UpdateUserAsync(UpdateProfileDto user,string userId)
        {
            var userProfile = await _context.ApplicationUsers.FirstOrDefaultAsync(x=>x.Id==userId);
            if (userProfile == null)
            { return null; }

            

            if (user.ProfileImage != null)
            {
                if (!string.IsNullOrEmpty(userProfile.ProfileImageUrl))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, "UserImages", userProfile.ProfileImageUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                userProfile.ProfileImageUrl = await GetUserProfileImageName(user.ProfileImage);
            }
            else { 
            userProfile.ProfileImageUrl=user.ProfileImageUrl;
            }

            userProfile.FullName = user.FullName;
            userProfile.LicenseExpiryDate=user.LicenseExpiryDate;
            userProfile.UserName=user.UserName;
            userProfile.PhoneNumber=user.PhoneNumber;
            userProfile.Country=user.Country;
            userProfile.City=user.City;
            userProfile.PostalCode=user.PostalCode;
            userProfile.DrivingLicenseNumber=user.DrivingLicenseNumber;


            await _context.SaveChangesAsync();
            return userProfile;



        }
        //Update only image:
        public async Task<string> UpdateProfileImage(string userId, UpdateProfileImage dto)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) 
            {
                throw new Exception("User Not found");
            }
            if (dto.ProfileFile!=null) 
            {
                var folderPath = Path.Combine(_env.WebRootPath, "UserImages");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //Delete old image:
                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath,user.ProfileImageUrl);
                    Console.WriteLine(oldImagePath);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                //save new image:
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ProfileFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ProfileFile.CopyToAsync(fileStream);
                }
                user.ProfileImageUrl = "UserImages/" + fileName;
                await _context.SaveChangesAsync();
                     }
            return user.ProfileImageUrl;
        }

        public async Task<bool> AddToWishlistAsync(string userId, int carId)
        {//wishList Log:
            _logger.LogInformation($"User {userId} adding car {carId} to wishlist");
            var exists = await _context.Wishlists.FirstOrDefaultAsync(x => x.UserId == userId && x.CarId == carId);
            if (exists != null) {
                _logger.LogWarning($"Failed to add car {carId} to wishlist for user {userId} it already exist");
                return false;
            }
            var wishlist = new Wishlist
            {
                UserId=userId,

CarId=carId            };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added to wishlist of {userId}");
            return true;
        }

        public async Task<bool> RemoveWishList(string userId, int carId)
        {
            var item = await _context.Wishlists.FirstOrDefaultAsync(x => x.UserId == userId && x.CarId == carId);
            if (item == null)
            {
                return false;
            }
            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
