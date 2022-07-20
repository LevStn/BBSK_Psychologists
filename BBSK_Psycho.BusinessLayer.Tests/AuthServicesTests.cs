

using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Moq;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class AuthServicesTests
    {
        private AuthServices _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;
        private Mock<IPsychologistsRepository> _psychologistsRepository;


        [SetUp]
        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _psychologistsRepository = new Mock<IPsychologistsRepository>();
            _sut = new AuthServices(_clientsRepositoryMock.Object, _psychologistsRepository.Object);
        }


        [Test]
        public void Login_ValidManagerEmailAndPassword_ClaimModelReturned()
        {
            //given
            var managerEmail = "manager@p.ru";
            var managerPassword = "Manager777";
            //when

            var claim= _sut.GetUserForLogin(managerEmail, managerPassword);
            //then

            Assert.True(claim.Role == "Manager");
            Assert.True(claim.Email == "manager@p.ru");


        }

        [Test]
        public void Login_ValidClientEmailAndPassword_ClaimModelReturned()
        {
            //given
            var clientExpected = new Client()
            {
                Name = "Petro",
                LastName = "Petrov",
                Password = "12345678",
                Email = "J@gmail.com",
                PhoneNumber = "89119118696",
                BirthDate = new DateTime(2020, 05, 05),
            };

            _clientsRepositoryMock.Setup(c => c.GetClientByEmail("J@gmail.com")).Returns(clientExpected);


            //when
            var claim = _sut.GetUserForLogin(clientExpected.Email, clientExpected.Password);

            //then
            Assert.True(claim.Role == "Client");
            Assert.True(claim.Email == clientExpected.Email);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
            _psychologistsRepository.Verify(c => c.GetPsychologistByEmail(It.IsAny<string>()), Times.Once);

        }

        //[Test]
        //public void Login_ValidPsychologistsEmailAndPassword_ClaimModelReturned()
        //{
        //    //given
        //    var psychologistsExpected = new Psychologist()
        //    {
        //        Name = "Petro",
        //        LastName = "Petrov",
        //        Password = "12345678",
        //        Email = "J@gmail.com",
        //        PhoneNumber = "89119118696",
        //        BirthDate = new DateTime(2020, 05, 05),
        //    };

        //    _clientsRepositoryMock.Setup(c => c.GetClientByEmail("J@gmail.com")).Returns(clientExpected);


        //    //when
        //    var claim = _sut.GetUserForLogin(clientExpected.Email, clientExpected.Password);

        //    //then
        //    Assert.True(claim.Role == "Client");
        //    Assert.True(claim.Email == clientExpected.Email);
        //    _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
        //    _psychologistsRepository.Verify(c => c.GetPsychologistByEmail(It.IsAny<string>()), Times.Once);


        //}
    }
}
