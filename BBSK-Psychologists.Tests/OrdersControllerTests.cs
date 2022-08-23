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
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho;
using BBSK_Psycho.Models.Responses;
using System.Security.Claims;

namespace BBSK_Psychologists.Tests
{
    public class OrdersControllerTests
    {
        private OrdersController _sut;
        private Mock<OrdersController> _ordersController;
        private Mock<IOrdersService> _ordersService;
        private ClaimModel _claimModel;
        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigStorage>()));
            _ordersController = new Mock<OrdersController>();
            _ordersService = new Mock<IOrdersService>();
            _sut = new OrdersController(_ordersService.Object, _mapper);
            _claimModel = new ClaimModel();

            ClaimsPrincipal user = new();
            _sut.ControllerContext = new ControllerContext();
            _sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        }

        [Test]
        public async Task GetOrders_ValidRolePassed_RequestedTypeReceived()
        {
            //given
            List<Order> allOrders = new List<Order>() { OrdersHelper.GetOrder(), OrdersHelper.GetOrder() };
            
            _claimModel.Role = Role.Manager;

            _sut.ControllerContext.HttpContext.User = OrdersHelper.GetUser("go@v.no", Role.Manager, 58);

            _ordersService.Setup(c => c.GetOrders(_claimModel)).ReturnsAsync(allOrders);

            //when
            var actual = await _sut.GetOrders();

            //then
            var actualResult = actual.Result as ObjectResult;
            var actualValues = actualResult.Value as List<AllOrdersResponse>;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            
            Assert.IsNotNull(actual);

            for(int i = 0; i < allOrders.Count; i++)
            {
                Assert.AreEqual(actualValues[i].Duration, allOrders[i].Duration);
                Assert.AreEqual(actualValues[i].IsDeleted, allOrders[i].IsDeleted);
                Assert.AreEqual(actualValues[i].Message, allOrders[i].Message);
                Assert.AreEqual(actualValues[i].OrderDate, allOrders[i].OrderDate);
                Assert.AreEqual(actualValues[i].OrderPaymentStatus, allOrders[i].OrderPaymentStatus);
                Assert.AreEqual(actualValues[i].OrderStatus, allOrders[i].OrderStatus);
                Assert.AreEqual(actualValues[i].PayDate, allOrders[i].PayDate);
                Assert.AreEqual(actualValues[i].SessionDate, allOrders[i].SessionDate);
            }

            _ordersService.Verify(c => c.GetOrders(_claimModel), Times.Once);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Client)]
        [TestCase(Role.Psychologist)]
        public async Task GetOrderById_ValidIdPassed_OkReceived(Role role)
        {
            //given
            Order expectedOrder = OrdersHelper.GetOrder();

            _claimModel.Role = role;

            _ordersService.Setup(c => c.GetOrderById(expectedOrder.Id, _claimModel)).ReturnsAsync(expectedOrder);

            _sut.ControllerContext.HttpContext.User = OrdersHelper.GetUser("go@v.no", role, 58);

            //when
            var actual = await _sut.GetOrderById(expectedOrder.Id);

            //then
            var actualResult = actual.Result as ObjectResult;
            var actualValue = actualResult.Value as OrderResponse;


            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.AreEqual(actualResult.Value.GetType(), typeof(OrderResponse));

            Assert.AreEqual((SessionDuration)actualValue.Duration, expectedOrder.Duration);
            Assert.AreEqual(actualValue.Message, expectedOrder.Message);
            Assert.AreEqual(actualValue.OrderDate, expectedOrder.OrderDate);
            Assert.AreEqual(actualValue.OrderPaymentStatus, expectedOrder.OrderPaymentStatus);
            Assert.AreEqual(actualValue.OrderStatus, expectedOrder.OrderStatus);
            Assert.AreEqual(actualValue.PayDate, expectedOrder.PayDate);
            Assert.AreEqual(actualValue.SessionDate, expectedOrder.SessionDate);

            _ordersService.Verify(c => c.GetOrderById(expectedOrder.Id, _claimModel), Times.Once);
        }

        [TestCase(Role.Manager)]
        [TestCase(Role.Client)]
        public async Task AddOrder_ValidRequestPassed_CreatedResultReceived(Role role)
        {
            //given
            _claimModel.Role = role;
            _claimModel.Id = 228;
            _sut.ControllerContext.HttpContext.User = OrdersHelper.GetUser("go@v.no", role, 228);

            Client client = OrdersHelper.GetClient();
            client.Id = _claimModel.Id;
            Psychologist psychologist = OrdersHelper.GetPsychologist();

            OrderCreateRequest givenRequest = new()
            {
                //ClientId = client.Id,
                Cost = 1100,
                Duration = SessionDuration.OneAcademicHour,
                Message = "",
                OrderDate = DateTime.Now,
                OrderPaymentStatus = OrderPaymentStatus.Paid,
                OrderStatus = OrderStatus.Created,
                PayDate = DateTime.Now,
                PsychologistId = psychologist.Id,
                SessionDate = DateTime.Now
            };

            Order expectedOrder = _mapper.Map<Order>(givenRequest);

            expectedOrder.Client = client;
            expectedOrder.Psychologist = psychologist;

            _ordersService.Setup(c => c.AddOrder(expectedOrder, _claimModel));

            //when
            var actual = await _sut.AddOrder(givenRequest);

            //then
            var actualResult = actual.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

            _ordersService.Verify(c => c.AddOrder(expectedOrder, _claimModel), Times.Once);
        }

        [Test]
        public async Task DeleteOrderById_ValidIdPassed_NoContentReceived()
        {
            //given
            Order givenOrder = OrdersHelper.GetOrder();

            _sut.ControllerContext.HttpContext.User = OrdersHelper.GetUser("go@v.no", Role.Manager, 58);
            _claimModel.Role = Role.Manager;

            _ordersService.Setup(c => c.DeleteOrder(givenOrder.Id, _claimModel));

            //when
            var actual = await _sut.DeleteOrderById(givenOrder.Id);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _ordersService.Verify(c => c.DeleteOrder(givenOrder.Id, _claimModel), Times.Once);
        }

        [Test]
        public async Task UpdateOrderStatusById_ValidRequestAndIdPassed_NoContentReceived()
        {
            //given
            Order givenOrder = OrdersHelper.GetOrder();

            _sut.ControllerContext.HttpContext.User = OrdersHelper.GetUser("go@v.no", Role.Manager, 58);
            _claimModel.Role = Role.Manager;

            var givenRequest = new OrderStatusPatchRequest() 
            { 
                OrderPaymentStatus = OrderPaymentStatus.MoneyReturned,
                OrderStatus = OrderStatus.Cancelled
            };

            _ordersService.Setup(c => c.UpdateOrderStatuses(givenOrder.Id, givenRequest.OrderStatus, givenRequest.OrderPaymentStatus, _claimModel));

            //when
            var actual = await _sut.UpdateOrderStatusById(givenOrder.Id, givenRequest);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _ordersService.Verify(c => c.UpdateOrderStatuses(givenOrder.Id, givenRequest.OrderStatus, givenRequest.OrderPaymentStatus, _claimModel), Times.Once);
        }
    }
}