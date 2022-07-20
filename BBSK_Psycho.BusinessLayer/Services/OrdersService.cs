using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services
{
    public class OrdersService : IOrdersService
    {
        public List<Order> GetAllOrders() { return null; }

        public Order? GetOrderById() { return null; }

        public int AddOrder() { return 0; }

        public void DeleteOrderById() { }

        public void UpdateOrderStatusById() { }
    }
}
