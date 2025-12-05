using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarOrderApi.Services
{
    public class TokenServices
    {
        private readonly IConfiguration _config;

        public TokenServices(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(IdentityUser user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:creds

                )
;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
