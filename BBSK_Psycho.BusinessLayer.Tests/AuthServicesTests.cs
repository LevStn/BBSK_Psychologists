

using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
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

        [Test]
        public void Login_ValidPsychologistsEmailAndPassword_ClaimModelReturned()
        {
            //given
            var psychologistsExpected = new Psychologist()
            {
                Name = "Dantes",
                LastName = "Don",
                Patronymic = "Petrovich",
                Gender = Gender.Male,
                Phone = "891198883526",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "12334534"
            };


            _psychologistsRepository.Setup(c => c.GetPsychologistByEmail("ros@fja.com")).Returns(psychologistsExpected);

            //when
            var claim = _sut.GetUserForLogin(psychologistsExpected.Email, psychologistsExpected.Password);

            //then
            Assert.True(claim.Role == "Psychologist");
            Assert.True(claim.Email == psychologistsExpected.Email);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
            _psychologistsRepository.Verify(c => c.GetPsychologistByEmail(It.IsAny<string>()), Times.Once);
        }


        [Test]
        public void Login_BadPassword_ThrowEntityNotFoundException()
        {
            //given
            var badPassword = "123456789541";
            var psychologistsExpected = new Psychologist()
            {
                Name = "Dantes",
                LastName = "Don",
                Patronymic = "Petrovich",
                Gender = Gender.Male,
                Phone = "891198883526",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "12334534"
            };

            _psychologistsRepository.Setup(c => c.GetPsychologistByEmail("ros@fja.com")).Returns(psychologistsExpected);


            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetUserForLogin(psychologistsExpected.Email, badPassword));
            
        }

        [Test]
        public void Login_UserNotFound_ThrowEntityNotFoundException()
        {
            //given
            var badEmail = "ad@mmm.com";
            var clientExpected = new Client()
            {
                Name = "Petro",
                LastName = "Petrov",
                Password = "12345678",
                Email = "J@gmail.com",
                PhoneNumber = "89119118696",
                BirthDate = new DateTime(2020, 05, 05),
            };

            _clientsRepositoryMock.Setup(c => c.GetClients());


            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetUserForLogin(badEmail, clientExpected.Password));

        }

    }
}

