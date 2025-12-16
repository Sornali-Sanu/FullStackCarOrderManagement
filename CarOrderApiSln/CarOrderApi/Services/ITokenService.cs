using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity;

namespace CarOrderApi.Services
{
    public interface ITokenService
    {
       Task <string> GenerateAccessToken(IdentityUser user);
        RefreshToken GetRefreshToken(string userId);
    }
}
