using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity;

namespace CarOrderApi.ExtentionProgramee_cs
{
    public static class IdentitySeeder
    {
        public static async Task SeedRoleAndAminAsync(this IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            //roles:
            string[] roles = { "Admin","User","Customer"};
            foreach (var role in roles) 
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //Admin User:
            var adminEmail = "Sornali456@gmail.com";
            var adminUser=await userManager.FindByNameAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed =true
                };
                var result= await userManager.CreateAsync(user,"Admin@123");
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
              
            }
        }
    }
}
