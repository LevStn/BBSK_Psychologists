using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Validators
{
    public class OrdersValidator : IOrdersValidator
    {
        public OrdersValidator() { }

        public void CheckClaimForRoles(ClaimModel claim, params Role[] roles)
        {
            if (!roles.Contains(claim.Role))
                throw new AccessDeniedException("Доступ запрещён");
        }
        public void CheckClaimForEmail(ClaimModel claim, Order order)
        {
            if (!(order.Client.Email == claim.Email ||
                  order.Psychologist.Email == claim.Email ||
                   claim.Role == Role.Manager))
                throw new AccessDeniedException("Доступ запрещён");
        }

        public void IsOrderValid(Order order)
        {
            if (order.IsDeleted)
                throw new DataException($"Нельзя добавить удалённый заказ");
            else if (order.Cost < order.Psychologist.Price)
                throw new DataException($"Цена не может быть ниже ставки психолога");
            else if (order.SessionDate < order.OrderDate)
                throw new DataException($"Дата оказания услуги не может быть раньше даты создания заказа");
            else if (order.SessionDate > order.OrderDate.AddMonths(1))
                throw new DataException("Дата оказания услуги не может быть позднее, чем через 1 месяц от даты создания заказа");
            else if (order.PayDate < order.OrderDate)
                throw new DataException($"Дата оплаты заказа не может быть раньше даты создания заказа");
            else if (order.SessionDate < order.PayDate)
                throw new DataException($"Услуга должна быть оказана после оплаты закака");
            else if (!Enum.IsDefined(typeof(SessionDuration), order.Duration))
                throw new DataException($"Неверно указана длительность консультации");
            else if (order.Message.Trim() == "")
                throw new DataException($"Неверно указано сообщение для психолога");
        }

        public void AreOrderStatusesValid(OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus)
        {
            if (!(orderStatus == OrderStatus.Completed && orderPaymentStatus == OrderPaymentStatus.Paid ||
                  orderStatus == OrderStatus.Created && orderPaymentStatus == OrderPaymentStatus.Paid ||
                  orderStatus == OrderStatus.Created && orderPaymentStatus == OrderPaymentStatus.Unpaid ||
                  orderStatus == OrderStatus.Cancelled && orderPaymentStatus == OrderPaymentStatus.Unpaid ||
                  orderStatus == OrderStatus.Cancelled && orderPaymentStatus == OrderPaymentStatus.MoneyReturned ||
                  orderStatus == OrderStatus.Deleted && orderPaymentStatus == OrderPaymentStatus.MoneyReturned ||
                  orderStatus == OrderStatus.Deleted && orderPaymentStatus == OrderPaymentStatus.Unpaid))
                throw new DataException($"Статус заказа {orderStatus} и статус оплаты {orderPaymentStatus} не могут быть использованы вместе");
        }
    }
}
