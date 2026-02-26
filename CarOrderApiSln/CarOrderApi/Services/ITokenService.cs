using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity;

namespace CarOrderApi.Services
{
    public interface ITokenService
    {
       Task <string> GenerateAccessToken(ApplicationUser user);
        RefreshToken GetRefreshToken(string userId);
    }
}
