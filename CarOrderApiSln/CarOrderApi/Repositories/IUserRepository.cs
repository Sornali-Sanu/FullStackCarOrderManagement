using CarOrderApi.Dtos;
using CarOrderApi.Model;

namespace CarOrderApi.Repositories
{
    public interface IUserRepository
    {
        Task<ProfileResponseDto> GetUserByIdAsync(string userId);
        Task<ApplicationUser> UpdateUserAsync(UpdateProfileDto user,string userId);
        Task<List<Order>> GetUserOrderAsync(string userId);
        Task<List<Wishlist>> GetUserWishlistAsync(string userId);
        Task AddToWishlistAsync(Wishlist wishlist);
    }
}
