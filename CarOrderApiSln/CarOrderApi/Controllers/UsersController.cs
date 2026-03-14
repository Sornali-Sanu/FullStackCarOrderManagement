using CarOrderApi.Dtos.UserDtos;
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
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService service,ILogger<UsersController>logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _logger.LogWarning($"Unauthorized access attemp.");
                return Unauthorized();
            }
            var profile=await _service.GetProfile(userId);
            if (profile == null)
            {
                _logger.LogWarning($"User not found with id{userId}");
                return NotFound();
            }
            _logger.LogInformation($"Profile fetched Successfully with id {userId}");
            return Ok(profile);
        }
        [HttpPut("UpdateProfile")]
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
        [Authorize]
        [HttpPut("UpdateProfileImage")]
        public async Task<IActionResult> UpdateProfileImageOnly([FromForm] UpdateProfileImage dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var imageUrl = await _service.UpdateUserImage(userId, dto);
            return Ok(new {
            massege="profile Updated Successfully",
            imageUrl=imageUrl

            });

        }
        [HttpPost("AddWishList")]
        public async Task<IActionResult> AddToWishList(int carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.AddWishList(userId, carId);
            if (!result)
                return BadRequest();
            return Ok("added to wishlist");

        }
        [HttpGet("getWishList")]
        public async Task<IActionResult> GetAllWishList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await _service.GetWishlists(userId);
            return Ok(list);
        }

        [HttpDelete("RemoveWishlist")]
        public async Task<IActionResult> RemoveWishList(int carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.RemoveCarFromwishList(userId, carId);
            return Ok(result);
        }
    }
}
