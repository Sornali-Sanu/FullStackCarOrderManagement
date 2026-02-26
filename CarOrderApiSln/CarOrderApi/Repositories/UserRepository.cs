using CarOrderApi.Data;
using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(AppDbContext context,UserManager<ApplicationUser>userManager)
        {
            _context=context;
            _userManager=userManager;
        }
        public async Task AddToWishlistAsync(Wishlist wishlist)
        {
          await _context.Wishlists.AddAsync(wishlist);
          await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<List<Order>> GetUserOrderAsync(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).Include(x => x.Car).ToListAsync();
        }

        public async Task<List<Wishlist>> GetUserWishlistAsync(string userId)
        {
            return await _context.Wishlists.Where(x => x.UserId == userId).Include(x => x.Car).ToListAsync();
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
