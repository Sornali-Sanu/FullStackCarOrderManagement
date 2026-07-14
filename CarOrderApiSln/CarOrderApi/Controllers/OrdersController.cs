using CarOrderApi.Dtos;
using CarOrderApi.Dtos.Admin;
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
        [Authorize(Roles = "Customer,User,Admin")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            await _services.PlaceOrder(dto, userId);
            return Ok(new {
            success=true,
            message="Order Successfully"
            });
        }

        [Authorize(Roles = "Customer,User,Admin")]
        [HttpGet("MyOrder")]
        public async Task<IActionResult> MyOrder()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _services.GetMyOrders(customerId);
            return Ok(orders);
        }
        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            return Ok(await _services.GetOrders());
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateOrderStatus dto)
        {
            var res = await _services.UpdateOrderStatus(id, dto.Status);
            if (!res) return NotFound("order Not Found");
            return Ok(res);


        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _services.DeleteOrder(id);
            return Ok($"Order is Deleted");
        }
        [Authorize(Roles ="Admin,User,Customer")]

        [HttpPut("CancelOrder/{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute]int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var result = await _services.CancelOrder(orderId, userId);
            if (!result) 
            {
                return BadRequest(
                    new
                    {
                        success
                    = false,
                        message = "order can not be canceled"
                    });
            }
            return Ok(new {
            success=true,
            message="Oder cancelled successfully"
            });

        }
    }
}
