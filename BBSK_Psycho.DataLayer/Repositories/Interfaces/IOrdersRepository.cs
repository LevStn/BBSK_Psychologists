using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task<int> AddOrder(Order order);
        Task DeleteOrder(int id);
        Task<List<Order>> GetOrders();
        Task<Order?> GetOrderById(int id);
        Task UpdateOrderStatuses(int orderId, OrderStatus orderStatus, OrderPaymentStatus paymentStatus);
        Task<Order?> GetOrderByPsychIdAndClientId(int psychId, int clientId);
    }
}