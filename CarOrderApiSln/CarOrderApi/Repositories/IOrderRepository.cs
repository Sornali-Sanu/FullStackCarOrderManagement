using CarOrderApi.Model;

namespace CarOrderApi.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> PlaceOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrder();
        Task<IEnumerable<Order>> GetOrderByCustomerId(string customerId);
        Task<Order> GetOrderById(int id);
        Task<Order> UpdateOrderStatus(int orderId, string status);
        Task<Order> DeleteOrder(int id);
    }
}
