using CarOrderApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarOrderApi.Services
{
    public class TokenServices: ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        public TokenServices(IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

      

        public async Task<string> GenerateAccessToken(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email)
            };
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: creds

                )
;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GetRefreshToken(string userId)
        {
            return new RefreshToken {
            Token=Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires=DateTime.Now.AddDays(7),
            IsRevoked=false,
            UserId=userId
            };
        }
    }
}
