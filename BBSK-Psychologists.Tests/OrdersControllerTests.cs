using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using BBSK_Psycho.DataLayer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models;
using NUnit.Framework;
using AutoMapper;
using Moq;

namespace BBSK_Psychologists.Tests
{
    public class OrdersControllerTests
    {
        private OrdersController _sut;
        private Mock<IOrdersRepository> _ordersRepository;
        private Mock<IMapper> _mapper;


        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>();
            _ordersRepository = new Mock<IOrdersRepository>();
            _sut = new OrdersController(_ordersRepository.Object, _mapper.Object);
        }

        [Test]
        public void GetOrders_NoValidationRequired_RequestedTypeReceived()
        {
            //given
            var allOrders = new List<OrderResponse>();

            //when
            var actual = _sut.GetAllOrders();

            //then
            var actualResult = actual.Result as ObjectResult;


            Assert.IsNotNull(actualResult);
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        }

        [Test]
        public void GetOrderById_ValidIdPassed_OkReceived()
        {
            //given
            var requestedOrderId = 42;
            var expectedOrder = new OrderResponse();
            expectedOrder.Id = 42;

            //when
            var actual = _sut.GetOrderById(requestedOrderId);

            //then
            var actualResult = actual.Result as ObjectResult;


            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.AreEqual(expectedOrder.GetType(), actualResult.Value.GetType());
            Assert.AreEqual(expectedOrder.Id, requestedOrderId);
        }

        [Test]
        public void AddOrder_ValidRequestPassed_CreatedResultReceived()
        {
            //given
            var orderCreateRequest = new OrderCreateRequest()
            {
                ClientId = 42,
                Cost = 1200,
                Duration = SessionDuration.OneAcademicHour,
                Message = "Программирование на C++",
                SessionDate = DateTime.Now,
                OrderDate = DateTime.Now,
                OrderPaymentStatus = OrderPaymentStatus.Unpaid

            };
            var expectedId = 2;

            //when
            var actual = _sut.AddOrder(orderCreateRequest);

            //then
            var actualResult = actual.Result as ObjectResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
            Assert.AreEqual(expectedId, actualResult.Value);
        }

        [Test]
        public void DeleteOrderById_ValidIdPassed_NoContentReceived()
        {
            //given
            var orderId = 42;

            //when
            var actual = _sut.DeleteOrderById(orderId);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        }

        [Test]
        public void UpdateOrderStatusById_ValidRequestAndIdPassed_NoContentReceived()
        {
            //given
            var orderId = 42;
            var incomingOrder = new OrderStatusPatchRequest();

            //when
            var actual = _sut.UpdateOrderStatusById(orderId, incomingOrder);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        }
    }
}
