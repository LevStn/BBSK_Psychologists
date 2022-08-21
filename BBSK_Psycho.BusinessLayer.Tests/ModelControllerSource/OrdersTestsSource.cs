using BBSK_DataLayer.Tests.TestCaseSources;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Tests.ModelControllerSource
{
    public class OrdersTestsSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Order order = OrdersHelper.GetOrder();
            order.IsDeleted = true;
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.Cost = 300;
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.SessionDate = DateTime.MinValue;
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.SessionDate = order.OrderDate.AddDays(33);
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.PayDate = order.SessionDate.AddDays(8);
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.PayDate = DateTime.MinValue;
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.Duration = (SessionDuration)60;
            yield return new object[]
            {
                order
            };

            order = OrdersHelper.GetOrder();
            order.Message = "  ";
            yield return new object[]
            {
                order
            };
        }
    }
}
