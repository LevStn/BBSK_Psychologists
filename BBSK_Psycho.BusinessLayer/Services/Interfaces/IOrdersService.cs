using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IOrdersService
    {
        int AddOrder();
        void DeleteOrderById();
        List<Order> GetAllOrders();
        Order? GetOrderById();
        void UpdateOrderStatusById();
    }
}