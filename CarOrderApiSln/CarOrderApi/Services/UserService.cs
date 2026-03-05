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

        public async Task<ProfileResponseDto> GetProfile(string userId)
        {
            return await _repo.GetUserByIdAsync(userId);
           
            
        }

        public async Task<List<Wishlist>> GetWishlists(string userId)
        {
            return await _repo.GetUserWishlistAsync(userId);
        }

        public async Task<ApplicationUser> UpdateProfile(string userId, UpdateProfileDto dto)
        {
            return await _repo.UpdateUserAsync(dto, userId);
        }
    }
}
