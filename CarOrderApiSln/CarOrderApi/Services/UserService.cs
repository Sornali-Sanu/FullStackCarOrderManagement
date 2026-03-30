using CarOrderApi.Dtos.UserDtos;
using CarOrderApi.Model;
using CarOrderApi.Repositories;

namespace CarOrderApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
      
        public UserService(IUserRepository repo, ILogger<UserService> logger)
        {
            _repo = repo;
            
        }

        public async Task<bool> AddWishList(string userId, int carId)
        {
           return await _repo.AddToWishlistAsync(userId, carId);
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

        public async Task<bool> RemoveCarFromwishList(string userId, int carId)
        {
            return await _repo.RemoveWishList(userId,carId);
        }

        public async Task<ApplicationUser> UpdateProfile(string userId, UpdateProfileDto dto)
        {
            return await _repo.UpdateUserAsync(dto, userId);
        }

        public async Task<string> UpdateUserImage(string userId, UpdateProfileImage dto)
        {
            return await _repo.UpdateProfileImage(userId, dto);
        }
    }
}
