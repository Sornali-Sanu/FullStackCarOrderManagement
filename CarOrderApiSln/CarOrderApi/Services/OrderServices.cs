using CarOrderApi.Dtos;
using CarOrderApi.Model;
using CarOrderApi.Repositories;

namespace CarOrderApi.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly ICarRepository _carRepo;

        public OrderServices(IOrderRepository repo, ICarRepository carRepo)
        {
            _repo = repo;
            _carRepo = carRepo;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await _repo.DeleteOrder(id);
            return order;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetMyOrders(string customerId)
        {
            var orders = await _repo.GetOrderByCustomerId(customerId);
            return orders.Select(o => new OrderResponseDto
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
                Quantity=orderDto.Quantity,
                TotalPrice=car.Price*orderDto.Quantity,
                OrderDate=DateTime.UtcNow,
                UserId = userId,
                Status="Panding"

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
