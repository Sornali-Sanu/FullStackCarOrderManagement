using CarOrderApi.Dtos;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _services;

        public OrdersController(IOrderService services)
        {
            _services = services;
        }
        [Authorize(Roles ="Customer,User,Admin")]
        [HttpPost]
        public async Task<IActionResult>PlaceOrder(CreateOrderDto dto)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _services.PlaceOrder(dto, customerId);
            return Ok("order Successfully");
        }

        //[Authorize(Roles = "Customer,User")]
        [HttpGet("MyOrder")]
        public async Task<IActionResult> MyOrder()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _services.GetMyOrders(customerId);
            return Ok(orders);
        }
        //[Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        { 
            return Ok(await _services.GetOrders());
        }

        //[Authorize(Roles ="Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateOrderStatus dto)
        {
            var res = await _services.UpdateOrderStatus(id, dto.Status);
            if (!res) return NotFound("order Not Found");
            return Ok(res);

        }
    }
}
