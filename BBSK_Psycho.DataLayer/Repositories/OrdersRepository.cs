using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BBSK_PsychoContext _context;

        public OrdersRepository(BBSK_PsychoContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrders() => await _context.Orders.Where(o => !o.IsDeleted).ToListAsync();
        public async Task<Order?> GetOrderByPsychIdAndClientId(int psychId, int clientId) => await _context.Orders.FirstOrDefaultAsync(o => o.Psychologist.Id == psychId && o.Client.Id == clientId);

        public async Task<Order?> GetOrderById(int orderId) => await _context.Orders.Include(o => o.Client).Include(o => o.Psychologist).FirstOrDefaultAsync(o => o.Id == orderId);
        

        public async Task<int> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task DeleteOrder(int orderId)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            order.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatuses(int orderId, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            order.OrderStatus = orderStatus;
            order.OrderPaymentStatus = orderPaymentStatus;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}