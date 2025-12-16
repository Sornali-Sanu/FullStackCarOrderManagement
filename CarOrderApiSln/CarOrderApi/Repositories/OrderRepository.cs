using CarOrderApi.Data;
using CarOrderApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order=await _context.Orders.FindAsync(id);
            if (order != null) 
            
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _context.Orders.Include(o=>o.Car).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByCustomerId(string customerId)
        {
            return await _context.Orders.Include(o=>o.Car).Where(p=>p.CustomerId==customerId).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.Include(o => o.Car).FirstOrDefaultAsync(o=>o.OrderId==id);
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if(order==null)
            {
                return null;
            }
            order.Status = status;
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
