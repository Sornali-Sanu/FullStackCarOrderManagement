using CarOrderApi.Dtos;
using CarOrderApi.Model;
using CarOrderApi.Repositories;

namespace CarOrderApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task AddWishList(string userId, int carId)
        {
            var wishlist = new Wishlist {
            UserId=userId,
            CarId=carId};
            await _repo.AddToWishlistAsync(wishlist);
        }

        public async Task<List<Order>> GetOrders(string userId)
        {
            return await _repo.GetUserOrderAsync(userId);
        }

        public async Task<ApplicationUser> GetProfile(string userId)
        {
           return await _repo.GetUserByIdAsync(userId);
        }

        public async Task<List<Wishlist>> GetWishlists(string userId)
        {
            return await _repo.GetUserWishlistAsync(userId);
        }

        public async Task UpdateProfile(string userId, UpdateProfileDto dto)
        {//need to add image
            var user=await _repo.GetUserByIdAsync(userId);
            user.FullName= dto.FullName;
            user.PhoneNumber= dto.PhoneNumber;
            user.Country= dto.Country;
            user.City= dto.City;
            user.PostalCode= dto.PostalCode;
            user.StreetAddress= dto.StreetAddress;
            user.DrivingLicenseNumber= dto.DrivingLicenseNumber;
            user.LicenseExpiryDate= dto.LicenseExpiryDate;
            await _repo.UpdateUserAsync(user);
        }
    }
}
