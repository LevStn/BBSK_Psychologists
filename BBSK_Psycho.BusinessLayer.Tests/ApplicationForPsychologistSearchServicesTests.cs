using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Moq;

namespace BBSK_Psycho.BusinessLayer.Tests;

public class ApplicationForPsychologistSearchServicesTests
{
    private ApplicationForPsychologistSearchServices _sut;
    private Mock<IClientsRepository> _clientsRepositoryMock;
    private Mock<IApplicationForPsychologistSearchRepository> _applicationForPsychologistSearchRepositoryMock;

    private ClaimModel _claims;

    [SetUp]
    public void Setup()
    {

        _clientsRepositoryMock = new Mock<IClientsRepository>();
        _applicationForPsychologistSearchRepositoryMock = new Mock<IApplicationForPsychologistSearchRepository>();
        _sut = new ApplicationForPsychologistSearchServices(_applicationForPsychologistSearchRepositoryMock.Object, _clientsRepositoryMock.Object);
    }


    [Test]
    public void AddApplicationForPsychologist_ValidRequestPassed_AddAplicationAndIdReturned()
    {
        //given
        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.AddApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>()))
            .Returns(1);

        var expectedId = 1;

        var application = new ApplicationForPsychologistSearch()
        {
            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day
        };

        //when
        var actual = _sut.AddApplicationForPsychologist(application);

        //then
        Assert.True(actual == expectedId);
        _applicationForPsychologistSearchRepositoryMock.Verify(a => a.AddApplicationForPsychologist(application), Times.Once);
    }

    [Test]
    public void GetAllApplicationsForPsychologist_ValidRequestPassed_ApplicationsListReceived()
    {
        //given

        var application = new List<ApplicationForPsychologistSearch>()
        {
            new ApplicationForPsychologistSearch()
            {
                Id=1,
                Name = "Alla",
                PhoneNumber = "89119856375",
                Description = "give me a help",
                PsychologistGender = Gender.Male,
                CostMin = 100,
                CostMax = 200,
                Date = new DateTime(2022, 02, 02),
                Time = TimeOfDay.Day,
                IsDeleted=true
            },
            new ApplicationForPsychologistSearch()
            {
                Id =2,
                Name = "Alla",
                PhoneNumber = "89119856375",
                Description = "give me a help",
                PsychologistGender = Gender.Male,
                CostMin = 100,
                CostMax = 200,
                Date = new DateTime(2022, 02, 02),
                Time = TimeOfDay.Day
                
            },
        };
        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetAllApplicationsForPsychologist()).Returns(application);

        //when

        var actual = _sut.GetAllApplicationsForPsychologist();

        //then

        Assert.NotNull(actual);
        Assert.True(actual.GetType() == typeof(List<ApplicationForPsychologistSearch>));
        Assert.True(actual[0].IsDeleted);
        Assert.False(actual[1].IsDeleted);
        _applicationForPsychologistSearchRepositoryMock.Verify(c=>c.GetAllApplicationsForPsychologist(), Times.Once());

    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void GetApplicationForPsychologistById_ValidRequestPassed_ApplicationsReceived(string role)
    {
        //givet
        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id =2,
            Name = "Roma",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day,
            Client = new()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375"
            }
        };

        if (role == Role.Manager.ToString())
        {
            applicationInDb.Client.Email = null;
        }

        _claims = new() { Email = applicationInDb.Client.Email, Role = role };

        _applicationForPsychologistSearchRepositoryMock.Setup(a=>a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

        //when

        var actual = _sut.GetApplicationForPsychologistById(applicationInDb.Id, _claims);

        //then

        Assert.True(actual.Id == applicationInDb.Id);
        Assert.True(actual.Client.Id == applicationInDb.Client.Id);
        Assert.True(actual.IsDeleted == false);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(It.IsAny<int>()), Times.Once);

    }

    [Test]
    public void GetApplicationForPsychologistById_EmptyRequest_ThrowEntityNotFoundException()
    {
        //given
        var testId = 2;
        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Roma",
            PhoneNumber = "89119856375",
            Client = new()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375"
            }

        };
        _claims = new() { Email = applicationInDb.Client.Email, Role = Role.Client.ToString() };

        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);


        //when, then
        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetApplicationForPsychologistById(testId, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(It.IsAny<int>()), Times.Once);
    }


    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void GetApplicationForPsychologistById_ClientGetSomeoneElseProfileAndRolePsychologist_ThrowAccessException(string role)
    {
        //given
        var testEmail = "pp@mail.com";

        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Roma",
            PhoneNumber = "89119856375",
            Client = new()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375"
            }
        };

        if (role == Role.Psychologist.ToString())
        {
            testEmail = applicationInDb.Client.Email;
        }

        _claims = new() { Email = testEmail, Role = role };
        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

        //when, then
        Assert.Throws<Exceptions.AccessException>(() => _sut.GetApplicationForPsychologistById(applicationInDb.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(It.IsAny<int>()), Times.Once);
    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void GetApplicationsForPsychologistByClientId_ValidRequestPassed_ApplicationsReceived(string role)
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Vasya",
            Email = "a@gmail.com",
            ApplicationForPsychologistSearch = new()
            {
                new()
                {
                    Id=1,
                    Description ="Help"
                },
                 new()
                {
                    Id=2,
                    Description ="Hi"
                }
            }
        };
        

        if (role == Role.Manager.ToString())
        {
            client.Email = null;
        }
        _claims = new() { Email = client.Email, Role = role };

        _clientsRepositoryMock.Setup(c => c.GetClientById(client.Id)).Returns(client);

        //when
        var actual = _sut.GetApplicationsForPsychologistByClientId(client.Id, _claims);

        //then

        Assert.AreEqual(actual, client.ApplicationForPsychologistSearch);
        _clientsRepositoryMock.Verify(a => a.GetClientById(It.IsAny<int>()), Times.Once);
       
    }

    [Test]
    public void GetApplicationsForPsychologistByClientId_EmptyClientRequest_ThrowEntityNotFoundException()
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Vasya",
            Email = "a@gmail.com",
    
        };
        _claims = new() { Email = client.Email, Role = Role.Client.ToString()};

        

        //when, then
        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetApplicationsForPsychologistByClientId(client.Id, _claims));
        _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Once);

    }


    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void GetApplicationsForPsychologistByClientId_ClientGetSomeoneElseProfileAndRolePsychologist_ThrowAccessException(string role)
    {
        //given
        var testEmail = "pp@mail.com";

        var client = new Client()
        {
            Id = 1,
            Name = "Vasya",
            Email = "a@gmail.com",
            ApplicationForPsychologistSearch = new()
            {
                new()
                {
                    Id=1,
                    Description ="Help"
                },
                 new()
                {
                    Id=2,
                    Description ="Hi"
                }
            }
        };

        if (role == Role.Psychologist.ToString())
        {
            testEmail = client.Email;
        }

        _claims = new() { Email = testEmail, Role = role };
        _clientsRepositoryMock.Setup(o => o.GetClientById(client.Id)).Returns(client);

        //when, then
        Assert.Throws<Exceptions.AccessException>(() => _sut.GetApplicationsForPsychologistByClientId(client.Id, _claims));
        _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Once);
    }
}
