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
            {
                _context.Database.EnsureDeleted();
            }

            _context = new BBSK_PsychoContext(_dbContextOptions);

            _sut = new OrdersRepository(_context);
        }

        [Test]
        public void GetOrders_WhenCorrectEndpointCalled_ThenOrdersListReturned()
        {
            //given

            Client client = OrdersHelper.GetClient();
            Psychologist psychologist = OrdersHelper.GetPsychologist();
            Order firstOrder = OrdersHelper.GetOrder(client, psychologist);
            Order secondOrder = OrdersHelper.GetOrder();

            _context.Orders.Add(firstOrder);
            _context.Orders.Add(secondOrder);
            _context.SaveChanges();

            //when
            List<Order> orders = _sut.GetOrders();


            //then
            Assert.NotNull(orders);
            Assert.That(orders != null);

            Assert.That(orders, Contains.Item(firstOrder));
            Assert.That(orders, Contains.Item(secondOrder));

            Assert.That(firstOrder.ClientId == client.Id);
            Assert.That(firstOrder.Client.Id == client.Id);
            Assert.That(firstOrder.PsychologistId == psychologist.Id);
            Assert.That(firstOrder.Psychologist.Id == psychologist.Id);
        }
    }
}