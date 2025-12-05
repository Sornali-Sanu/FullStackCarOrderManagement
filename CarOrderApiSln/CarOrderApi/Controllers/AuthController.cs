using CarOrderApi.Dtos;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenServices _tokenService;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            TokenServices tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

            return Ok("Registration successful");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password");

            var token = _tokenService.CreateToken(user);

            return Ok(new
            {
                token = token,
                email = user.Email,
                userId = user.Id
            });
        }
    }
}

