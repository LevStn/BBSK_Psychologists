using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class ManagerRepositoryTests
    {

        private DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

        private ManagerRepository _sut;
        private BBSK_PsychoContext context;

        public ManagerRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

        }

        [SetUp]
        public void Setup()
        {

            if (context is not null)
                context.Database.EnsureDeleted();


            context = new BBSK_PsychoContext(_dbContextOptions);

            _sut = new ManagerRepository(context);

        }

        [Test]
        public void GetManagerByEmail_WhenCorrectEmail_ManagerReturned()
        {
            //given
            var expectedManagerFirst = new Manager()
            {

                Email = "Va@gmail.com",
                Password = "12345678dad"

            };

            var expectedManagerSecond = new Manager()
            {

                Email = "aaa@gmail.com",
                Password = "12345678dad"
            };

            context.Managers.Add(expectedManagerFirst);
            context.Managers.Add(expectedManagerSecond);
            context.SaveChanges();

            //when
            var actualClient = _sut.GetManagerByEmail(expectedManagerSecond.Email);

            //then

            Assert.True(actualClient.Id == expectedManagerSecond.Id);
            Assert.True(actualClient.Email == expectedManagerSecond.Email);
        }
    }
}
