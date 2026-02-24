using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RefreshToken>RefreshTokens { get; set; }
        public DbSet<Wishlist>Wishlists { get; set; }
        public DbSet<ApplicationUser>ApplicationUsers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
               .HasOne(o => o.Car)
               .WithMany()
               .HasForeignKey(o => o.CarId);
        }
    }
}
