using BBSK_DataLayer.Tests.TestCaseSources;
using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.BusinessLayer.Tests.ModelControllerSource;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class OrdersValidatorTests
    {
        private IOrdersValidator _ordersValidator;
        private ClaimModel _claimModel;

        [SetUp]
        public void Setup()
        {
            _ordersValidator = new OrdersValidator();
            _claimModel = new ClaimModel();
        }

        [Test]
        public void IsOrderValid_ValidOrderPassed_NoExceptionThrown()
        {
            //given
            Order order = OrdersHelper.GetOrder();

            //when-then
            Assert.DoesNotThrow(() => _ordersValidator.IsOrderValid(order));
        }

        [TestCaseSource(typeof(OrdersTestsSource))]
        public void IsOrderValid_InvalidOrderPassed_InvalidDataExceptionThrown(Order givenOrder)
        {
            //given-when-then
            Assert.Throws<DataException>(() => _ordersValidator.IsOrderValid(givenOrder));
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Client)]
        [TestCase(Role.Psychologist)]
        public void CheckClaimForRoles_ValidRolePassed_NoExceptionThrown(Role role)
        {
            //given
            _claimModel.Role = role;

            //when-then
            Assert.DoesNotThrow(() => _ordersValidator.CheckClaimForRoles(_claimModel, role));
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Client)]
        [TestCase(Role.Psychologist)]
        public void CheckClaimForRoles_InvalidRolePassed_AccessDeniedExceptionThrown(Role role)
        {
            //given
            _claimModel.Role = role;

            //when-then
            if (role == Role.Manager)
                Assert.Throws<AccessDeniedException>(() => _ordersValidator.CheckClaimForRoles(_claimModel, Role.Client));
            else
                Assert.Throws<AccessDeniedException>(() => _ordersValidator.CheckClaimForRoles(_claimModel, Role.Manager));
        }

        [Test]
        public void CheckClaimForEmail_Clientincoming_NoExceptionThrown()
        {
            //given 
            Order order = OrdersHelper.GetOrder();
            order.Client = OrdersHelper.GetClient();
            order.Psychologist = OrdersHelper.GetPsychologist();

            _claimModel.Email = order.Client.Email;

            //when-then
            Assert.DoesNotThrow(() => _ordersValidator.CheckClaimForEmail(_claimModel, order));
        }

        [Test]
        public void CheckClaimForEmail_ManagerIncoming_NoExceptionThrown()
        {
            //given 
            Order order = OrdersHelper.GetOrder();
            order.Client = OrdersHelper.GetClient();
            order.Psychologist = OrdersHelper.GetPsychologist();

            _claimModel.Role = Role.Manager;

            //when-then
            Assert.DoesNotThrow(() => _ordersValidator.CheckClaimForEmail(_claimModel, order));
        }

        [Test]
        public void CheckClaimForEmail_PsychologistIncoming_NoExceptionThrown()
        {
            //given 
            Order order = OrdersHelper.GetOrder();
            order.Client = OrdersHelper.GetClient();
            order.Psychologist = OrdersHelper.GetPsychologist();

            _claimModel.Email = order.Psychologist.Email;

            //when-then
            Assert.DoesNotThrow(() => _ordersValidator.CheckClaimForEmail(_claimModel, order));
        }

        [TestCase(OrderStatus.Created, OrderPaymentStatus.Paid)]
        [TestCase(OrderStatus.Created, OrderPaymentStatus.Unpaid)]
        [TestCase(OrderStatus.Completed, OrderPaymentStatus.Paid)]
        [TestCase(OrderStatus.Cancelled, OrderPaymentStatus.Unpaid)]
        [TestCase(OrderStatus.Cancelled, OrderPaymentStatus.MoneyReturned)]
        [TestCase(OrderStatus.Deleted, OrderPaymentStatus.MoneyReturned)]
        [TestCase(OrderStatus.Deleted, OrderPaymentStatus.Unpaid)]
        public void AreOrderStatusesValid_ValidOrderStatusesPassed_NoExceptinThrown(OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus)
        {
            //given-when-then
            Assert.DoesNotThrow(() => _ordersValidator.AreOrderStatusesValid(orderStatus, orderPaymentStatus));
        }

        [TestCase(OrderStatus.Created, OrderPaymentStatus.MoneyReturned)]
        [TestCase(OrderStatus.Completed, OrderPaymentStatus.Unpaid)]
        [TestCase(OrderStatus.Completed, OrderPaymentStatus.MoneyReturned)]
        [TestCase(OrderStatus.Cancelled, OrderPaymentStatus.Paid)]
        [TestCase(OrderStatus.Deleted, OrderPaymentStatus.Paid)]
        public void AreOrderStatusesValid_InvalidOrderStatusesPassed_DataExceptinThrown(OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus)
        {
            //given-when-then
            Assert.Throws<DataException>(() => _ordersValidator.AreOrderStatusesValid(orderStatus, orderPaymentStatus));
        }
    }
}