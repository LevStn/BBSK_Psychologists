using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_DataLayer.Tests.TestCaseSources;

namespace BBSK_DataLayer.Tests
{
    public class OrdersRepositoryTests
    {
        private DbContextOptions<BBSK_PsychoContext> _dbContextOptions;
        private BBSK_PsychoContext _context;
        private OrdersRepository _sut;

        public OrdersRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;
        }

        [SetUp]
        public void Setup()
        {
            if(_context != null)
                _context.Database.EnsureDeleted();

            _context = new BBSK_PsychoContext(_dbContextOptions);
            _sut = new OrdersRepository(_context);
        }

        [Test]
        public async Task GetOrders_WhenCorrectEndpointCalled_ThenOrdersListReturned()
        {
            //given
            Client client = await OrdersHelper.GetClient();
            Psychologist psychologist =await OrdersHelper.GetPsychologist();
            Order firstOrder =await OrdersHelper.GetOrder(client, psychologist);
            Order secondOrder =await OrdersHelper.GetOrder();
            Order thirdOrder =await OrdersHelper.GetOrder();
            thirdOrder.IsDeleted = true;

            _context.Orders.Add(firstOrder);
            _context.Orders.Add(secondOrder);
            _context.Orders.Add(thirdOrder);
            _context.SaveChanges();

            List<Order> actualOrders = new() {firstOrder, secondOrder};

            //when
            List<Order> expectedOrders = await _sut.GetOrders();


            //then
            Assert.NotNull(expectedOrders);
            Assert.That(expectedOrders, Does.Not.Contains(thirdOrder));

            Assert.False(expectedOrders[0].IsDeleted);
            Assert.False(expectedOrders[1].IsDeleted);
        }

        [Test]
        public async Task GetOrderById_WhenCorrectIdPassed_ThenOrderReturned()
        {
            //given
            Order givenOrder = await OrdersHelper.GetOrder();

            _context.Orders.Add(givenOrder);
            _context.SaveChanges();

            //when
            Order expectedOrder = await _sut.GetOrderById(givenOrder.Id);

            //then
            Assert.AreEqual(givenOrder, expectedOrder);
        }

        [Test]
        public async Task AddOrder_WhenCorrectDataPassed_ThenOrderAdded()
        {
            //given
            Order givenOrder = await OrdersHelper.GetOrder();

            //when
            await _sut.AddOrder(givenOrder);

            Order expectedOrder = _context.Orders.FirstOrDefault(o => o.Id == givenOrder.Id);
            
            //then
            Assert.AreEqual(givenOrder, expectedOrder);
            Assert.AreEqual(expectedOrder.Id, expectedOrder.Id);
        }

        [Test]
        public async Task DeleteOrder_WhenCorrectIdPassed_ThenOrderDeleted()
        {
            //given
            Order givenOrder = await OrdersHelper.GetOrder();

            _context.Orders.Add(givenOrder);
            _context.SaveChanges();

            //when
            await _sut.DeleteOrder(givenOrder.Id);

            Order expected = _context.Orders.FirstOrDefault(o => o.Id == givenOrder.Id);

            //then
            Assert.AreEqual(givenOrder.IsDeleted, expected.IsDeleted);
            Assert.That(expected.IsDeleted);
        }

        [Test]
        public async Task UpdateOrdersStatus_WhenCorrectDataPassed_ThenOrderStatusUpdated()
        {
            //given
            Order givenOrder = await OrdersHelper.GetOrder();
            givenOrder.OrderPaymentStatus = OrderPaymentStatus.Unpaid;
            givenOrder.OrderStatus = OrderStatus.Created;

            _context.Orders.Add(givenOrder);
            _context.SaveChanges();

            //when
            await _sut.UpdateOrderStatuses(givenOrder.Id, OrderStatus.Completed, OrderPaymentStatus.Paid);

            Order expectedOrder = _context.Orders.FirstOrDefault(o => o.Id == givenOrder.Id);

            //then
            Assert.That(expectedOrder.OrderStatus == OrderStatus.Completed 
                && expectedOrder.OrderPaymentStatus == OrderPaymentStatus.Paid);
        }
    }
}