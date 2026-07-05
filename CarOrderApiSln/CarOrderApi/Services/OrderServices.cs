using CarOrderApi.Data;
using CarOrderApi.Dtos;
using CarOrderApi.Model;
using CarOrderApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly ICarRepository _carRepo;
        private readonly AppDbContext _context;
        public OrderServices(IOrderRepository repo, ICarRepository carRepo,AppDbContext context)
        {
            _repo = repo;
            _carRepo = carRepo;
            _context = context;
        }
        
        public async Task<bool> CancelOrder(int orderId, string userId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x=>x.Id==orderId && x.UserId==userId);
            if (order == null)
            {
                return false;
            }
            if(order.Status!="Pending")
            {
                return false;
            }
            order.Status = "Cancelled";
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await _repo.DeleteOrder(id);
            return order;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetMyOrders(string customerId)
        {
            var orders = await _repo.GetOrderByCustomerId(customerId);
            return orders.Where(x=>x.UserId==customerId && x.Status!="Cancelled").Select(o => new OrderResponseDto
            {
                OrderId = o.Id,
                CarId = o.CarId,
                CarName = o.Car.Name,
                Brand=o.Car.Brand,
                Quantity=o.Quantity,
                OrderDate = o.OrderDate,
                Status = o.Status,
                CarImage=o.Car.ImageUrl,
                Totalprice=o.Car.Price*o.Quantity,
                ShippingAddress=o.ShippingAddress,
            });
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrders()
        {
            var order = await _repo.GetAllOrder();
            return order.Select(o => new OrderResponseDto
            {
                OrderId = o.Id,
                CarId = o.CarId,
                CarName = o.Car.Name,
                
                OrderDate = o.OrderDate,
                Status = o.Status,
            });
        }

        public async Task PlaceOrder(CreateOrderDto orderDto, string userId)
        {
            var car = await _carRepo.GetCarById(orderDto.CarId);
            if (car == null)
            {
                throw new Exception($"Car not found.CarId={orderDto.CarId}");
            }
            var order = new Order
            {
                CarId = orderDto.CarId,
                Quantity = orderDto.Quantity,
                TotalPrice = orderDto.TotalPrice* orderDto.Quantity,

                PhoneNumber = orderDto.PhoneNumber,
                ShippingAddress = orderDto.ShippingAddress,
                PaymentMethods = orderDto.PaymentMethods,

                OrderDate = DateTime.UtcNow,
                UserId = userId,
                Status = "Pending"

            };
            await _repo.PlaceOrder(order);
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _repo.UpdateOrderStatus(orderId, status);
            return order != null;
        }
    }
}
