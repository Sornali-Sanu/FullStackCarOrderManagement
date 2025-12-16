using CarOrderApi.Dtos;

namespace CarOrderApi.Services
{
    public interface IOrderService
    {
        Task PlaceOrder(CreateOrderDto orderDto, string customerId);
        Task <IEnumerable<OrderResponseDto>>GetMyOrders(string customerId);
        Task<IEnumerable<OrderResponseDto>> GetOrders();
        Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
