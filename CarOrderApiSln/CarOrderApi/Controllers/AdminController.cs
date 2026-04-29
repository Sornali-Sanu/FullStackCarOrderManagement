using CarOrderApi.Dtos;
using CarOrderApi.Dtos.Admin;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTheCar()
        {
            var car = await _service.GetCarAsync();
            return Ok(car);
        }
        [HttpPost]
        public async Task<IActionResult> AddCarByAdmin([FromForm] AdminCarDto car)
        {

            var newCar = await _service.AddCarAsync(car);
            return Ok(newCar);
        }
        [HttpGet("Dashboard")]
        public async Task<IActionResult> DashBoard()
        {
            var dashboard=await _service.GetDashboardAsync();
            return Ok(dashboard);
        }
    }

}
