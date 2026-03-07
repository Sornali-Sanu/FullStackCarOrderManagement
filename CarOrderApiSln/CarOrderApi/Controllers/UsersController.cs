using CarOrderApi.Dtos;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarOrderApi.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var profile=await _service.GetProfile(userId);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost("UpdateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm]UpdateProfileDto dto)
        {//I have used this to autometically find the logged user by ClainType.NameIdentifier.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not Logged in");
            }
             var updatedUser= await _service.UpdateProfile(userId
                 , dto);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
            
        }
    }
}
