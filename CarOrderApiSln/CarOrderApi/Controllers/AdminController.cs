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
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _service.GetAllOrdersAsync());

        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateOrderStatus dto)
        {
            var result = await _service.UpdateOrderStatusAsync(id, dto.Status);

            if (!result)
                return NotFound();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _service.DeleteOrder(id);

            if (!result)
                return NotFound();

            return Ok();
        }

    }

}
