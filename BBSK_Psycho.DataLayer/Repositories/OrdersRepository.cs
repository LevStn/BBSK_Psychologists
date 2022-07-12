using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BBSK_PsychoContext _context;

        public OrdersRepository(BBSK_PsychoContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Where(o => o.IsDeleted == false).ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public void DeleteOrderById(int id)
        {
            Order order = GetOrderById(id);

            order.IsDeleted = true;

            _context.SaveChanges();
        }

        public void UpdateOrderStatusById(int orderId, int orderStatus, int paymentStatus)
        {
            if (orderStatus > Enum.GetNames(typeof(OrderStatus)).Length ||
                paymentStatus > Enum.GetNames(typeof(OrderPaymentStatus)).Length)
            {
                throw new ArgumentException("Invalid arguments");
            }

            Order order = GetOrderById(orderId);
            order.OrderStatus = (OrderStatus)orderStatus;
            order.OrderPaymentStatus = (OrderPaymentStatus)paymentStatus;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
