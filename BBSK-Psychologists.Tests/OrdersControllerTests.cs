using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using BBSK_DataLayer.Tests.TestCaseSources;
using BBSK_Psycho.DataLayer.Entities;
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
            var allOrders = new List<Order>();
            allOrders.Add(OrdersHelper.GetOrder());
            allOrders.Add(OrdersHelper.GetOrder());

            _ordersRepository.Setup(c => c.GetOrders()).Returns(allOrders).Verifiable();

            //when
            var actual = _sut.GetAllOrders();

            //then
            var actualResult = actual.Result as ObjectResult;


            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.IsNotNull(actualResult);

            _ordersRepository.Verify(c => c.GetOrders(), Times.Once);

            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Never);
            _ordersRepository.Verify(c => c.DeleteOrder(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.UpdateOrderStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GetOrderById_ValidIdPassed_OkReceived()
        {
            //given
            var expectedOrder = OrdersHelper.GetOrder();

            _ordersRepository.Setup(c => c.GetOrderById(expectedOrder.Id)).Returns(expectedOrder);

            //when
            var actual = _sut.GetOrderById(expectedOrder.Id);

            //then
            var actualResult = actual.Result as ObjectResult;


            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.AreEqual(expectedOrder.GetType(), actualResult.Value.GetType());

            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Once);

            _ordersRepository.Verify(c => c.GetOrders(), Times.Never);
            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Never);
            _ordersRepository.Verify(c => c.DeleteOrder(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.UpdateOrderStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }

        [Test]
        public void AddOrder_ValidRequestPassed_CreatedResultReceived()
        {
            //given
            OrderCreateRequest givenRequest = new();
            _ordersRepository.Setup(c => c.AddOrder(It.IsAny<Order>())).Returns(1);

            //when
            var actual = _sut.AddOrder(givenRequest);

            //then
            var actualResult = actual.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Once);

            _ordersRepository.Verify(c => c.GetOrders(), Times.Never);
            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.DeleteOrder(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.UpdateOrderStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void DeleteOrderById_ValidIdPassed_NoContentReceived()
        {
            //given
            Order givenOrder = OrdersHelper.GetOrder();

            //when
            var actual = _sut.DeleteOrderById(givenOrder.Id);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _ordersRepository.Verify(c => c.DeleteOrder(It.IsAny<int>()), Times.Once);

            _ordersRepository.Verify(c => c.GetOrders(), Times.Never);
            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Never);
            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.UpdateOrderStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void UpdateOrderStatusById_ValidRequestAndIdPassed_NoContentReceived()
        {
            //given
            Order givenOrder = OrdersHelper.GetOrder();
            //givenOrder.Id = 42;

            var givenRequest = new OrderStatusPatchRequest() 
            { 
                OrderPaymentStatus = OrderPaymentStatus.MoneyReturned,
                OrderStatus = OrderStatus.Cancelled
            };

            _ordersRepository.Setup(c => c.UpdateOrderStatus(givenOrder.Id, (int)givenRequest.OrderPaymentStatus, (int)givenRequest.OrderStatus));

            //when
            var actual = _sut.UpdateOrderStatusById(givenOrder.Id, givenRequest);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _ordersRepository.Verify(c => c.UpdateOrderStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);

            _ordersRepository.Verify(c => c.GetOrders(), Times.Never);
            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Never);
            _ordersRepository.Verify(c => c.DeleteOrder(It.IsAny<int>()), Times.Never);
            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Never);
        }
    }
}
