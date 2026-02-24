using CarOrderApi.Dtos;
using CarOrderApi.Model;

namespace CarOrderApi.Services
{
    public interface IOrderService
    {
        Task PlaceOrder(CreateOrderDto orderDto, string userId);
        Task<IEnumerable<OrderResponseDto>> GetMyOrders(string userId);
        Task<IEnumerable<OrderResponseDto>> GetOrders();
        Task<bool> UpdateOrderStatus(int orderId, string status);
        Task<Order> DeleteOrder(int id);
    }
}
