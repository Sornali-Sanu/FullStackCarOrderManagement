using CarOrderApi.Dtos;
using CarOrderApi.Model;
using CarOrderApi.Repositories;

namespace CarOrderApi.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderServices(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetMyOrders(string customerId)
        {
            var orders=await _repo.GetOrderByCustomerId(customerId);
            return orders.Select(o =>  new OrderResponseDto
            {
                OrderId = o.OrderId,
                CarId= o.CarId,
                CarName=o.Car.Name,
                Quantity=o.Quantity,
                OrderDate=o.OrderDate,
                Status=o.Status,
                
            });
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrders()
        {
          var order=await _repo.GetAllOrder();
            return order.Select(o => new OrderResponseDto {
            OrderId=o.OrderId,
            CarId=o.CarId,
            CarName=o.Car.Name,
            Quantity=o.Quantity,
            OrderDate=o.OrderDate,
            Status =o.Status,
            });
        }

        public async Task PlaceOrder(CreateOrderDto orderDto, string customerId)
        {
            var order = new Order {
            CarId = orderDto.CarId,
            Quantity=orderDto.Quantity,
            CustomerId = customerId
            
            };
            await _repo.PlaceOrder(order);
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
           var order=await _repo.UpdateOrderStatus(orderId, status);
            return order!=null;
        }
    }
}
