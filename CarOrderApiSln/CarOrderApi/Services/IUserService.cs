using CarOrderApi.Dtos;
using CarOrderApi.Model;

namespace CarOrderApi.Services
{
    public interface IUserService
    {
        Task<ProfileResponseDto> GetProfile(string userId);
        Task<ApplicationUser> UpdateProfile(string userId,UpdateProfileDto dto);
        Task<List<Order>> GetOrders(string userId);
        Task<List<Wishlist>>GetWishlists(string userId);
        Task AddWishList(string userId, int carId);
        Task<string> UpdateUserImage(string userId, UpdateProfileImage dto);
    }
}
