using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<int> AddOrder(Order order, ClaimModel claim);
        Task DeleteOrder(int id, ClaimModel claim);
        Task<Order?> GetOrderById(int id, ClaimModel claim);
        Task<List<Order>> GetOrders(ClaimModel claim);
        Task UpdateOrderStatuses(int id, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus, ClaimModel claim);
    }
}