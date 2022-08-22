using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IOrdersValidator
    {
        void AreOrderStatusesValid(OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus);
        void CheckClaimForEmail(ClaimModel claim, Order order);
        void CheckClaimForRoles(ClaimModel claim, params Role[] roles);
        void IsOrderValid(Order order);
    }
}