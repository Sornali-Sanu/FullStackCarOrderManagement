using CarOrderApi.Data;
using CarOrderApi.Dtos;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace CarOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

      

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
           
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(user, "User");
            return Ok("Registration successful");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user,dto.Password))
                return Unauthorized("Invalid email or password");

            var accessToken =_tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GetRefreshToken(user.Id);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return Ok(new { accessToken, refreshToken = refreshToken.Token }

                );

           
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var storeToken = await _context.RefreshTokens.Include(x => x.User).FirstOrDefaultAsync(x => x.Token == refreshToken);
            if (storeToken == null || storeToken.IsRevoked || storeToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized();
            }
            //Token Rotation:
            storeToken.IsRevoked = true;
            var newAccesstoken =await _tokenService.GenerateAccessToken(storeToken.User);
            var newRefreshToken = _tokenService.GetRefreshToken(storeToken.UserId);
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                accessToken = newAccesstoken,
                refreshToken = newRefreshToken.Token
            });
        }

       
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] logoutDto dto)
        {
           
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);
            if (token == null) {
                return BadRequest();
            }
            token.IsRevoked = true;
            await _context.SaveChangesAsync();
            return Ok("Logged out");
        }
    }
}

