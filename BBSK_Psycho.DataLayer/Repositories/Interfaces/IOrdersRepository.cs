using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        int AddOrder(Order order);
        void DeleteOrderById(int id);
        List<Order> GetAllOrders();
        Order? GetOrderById(int id);
        void UpdateOrderStatusById(int orderId, int orderStatus, int paymentStatus);
    }
}