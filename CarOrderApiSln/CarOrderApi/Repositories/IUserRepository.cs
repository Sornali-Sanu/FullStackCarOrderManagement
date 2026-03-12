using CarOrderApi.Dtos.UserDtos;
using CarOrderApi.Model;

namespace CarOrderApi.Repositories
{
    public interface IUserRepository
    {
        Task<ProfileResponseDto> GetUserByIdAsync(string userId);
        Task<ApplicationUser> UpdateUserAsync(UpdateProfileDto user,string userId);
        Task<List<Order>> GetUserOrderAsync(string userId);
        //Getwishlist:
        Task<List<Wishlist>> GetUserWishlistAsync(string userId);
        //add wishList:
        Task<bool> AddToWishlistAsync(string userId,int carId);
        Task<string> UpdateProfileImage(string userId, UpdateProfileImage dto);
        
        
        //remove wishlist:
        Task<bool> RemoveWishList(string userId, int carId);

    }
}
