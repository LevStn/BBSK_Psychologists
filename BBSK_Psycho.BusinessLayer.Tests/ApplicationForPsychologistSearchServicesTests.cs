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
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetAllApplicationsForPsychologist(), Times.Once());

    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void GetApplicationForPsychologistById_ValidRequestPassed_ApplicationsReceived(string role)
    {
        //givet
        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id = 2,
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

        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

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
        _claims = new() { Email = client.Email, Role = Role.Client.ToString() };



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


    [TestCase("Client")]
    [TestCase("Manager")]
    public void UpdateUpdateApplicationForPsychologist_ValidRequestPassed_ChangesProperties(string role)
    {
        //given

        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Roma",
            PhoneNumber = "89119856375",
            Description = "Help",
            PsychologistGender = Gender.Famale,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 07, 30),
            Time = TimeOfDay.Evening,
            Client = new()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
            }
        };

        var newModel = new ApplicationForPsychologistSearch()
        {
            Id = 5,
            Name = "Zara",
            PhoneNumber = "89119850000",
            Description = "Net",
            PsychologistGender = Gender.Famale,
            CostMin = 100000,
            CostMax = 20000000,
            Date = new DateTime(2022, 12, 30),
            Time = TimeOfDay.Morning,
        };

        if (role == Role.Manager.ToString())
        {
            applicationInDb.Client.Email = null;
        }
        _claims = new() { Email = applicationInDb.Client.Email, Role = role };


        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);


        //when
        _sut.UpdateApplicationForPsychologist(newModel, applicationInDb.Id, _claims);

        //then

        var actual = _sut.GetApplicationForPsychologistById(applicationInDb.Id, _claims);

        Assert.True(newModel.Id != applicationInDb.Id);
        Assert.True(newModel.Name == applicationInDb.Name);
        Assert.True(newModel.PhoneNumber == applicationInDb.PhoneNumber);
        Assert.True(newModel.Description == applicationInDb.Description);
        Assert.True(newModel.Date == applicationInDb.Date);
        Assert.True(newModel.Time == applicationInDb.Time);
        Assert.True(newModel.PsychologistGender == applicationInDb.PsychologistGender);
        Assert.True(newModel.CostMax == applicationInDb.CostMax);
        Assert.True(newModel.CostMin == applicationInDb.CostMin);
        _applicationForPsychologistSearchRepositoryMock.Verify(a => a.GetApplicationForPsychologistById(It.IsAny<int>()), Times.Exactly(2));
        _applicationForPsychologistSearchRepositoryMock.Verify(a => a.UpdateApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>(), It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void UpdateUpdateApplicationForPsychologist_EmptyRequest_ThrowEntityNotFoundException()
    {
        //given
        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Client = new()
            {
                Email = "as@gmail.com"
            }
        };



        var newModel = new ApplicationForPsychologistSearch()
        {
            Id = 5,
            Name = "Zara",
            PhoneNumber = "89119850000",
            Description = "Net",
        };

        _claims = new() { Email = applicationInDb.Client.Email, Role = Role.Client.ToString() };

        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.UpdateApplicationForPsychologist(newModel, applicationInDb.Id));

        //when, then

        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.UpdateApplicationForPsychologist(newModel, applicationInDb.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.UpdateApplicationForPsychologist(newModel, applicationInDb.Id), Times.Never);
    }

    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void UpdateUpdateApplicationForPsychologist_ClientGetSomeoneElsesProfileAndRolePsychologist_ThrowAccessException(string role)
    {
        //given
        var testEmail = "bnb@gamil.ru";

        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Roma",
            PhoneNumber = "89119856375",
            Client = new()
            {
                Id = 1,
                Email = "Va@gmail.com",
            }
        };

        var newModel = new ApplicationForPsychologistSearch()
        {
            Id = 5,
            Name = "Zara",
            PhoneNumber = "89119850000",
        };

        if (role == Role.Psychologist.ToString())
        {
            testEmail = applicationInDb.Client.Email;
        }
        _claims = new() { Email = testEmail, Role = role };

        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

        //when, then

        Assert.Throws<Exceptions.AccessException>(() => _sut.UpdateApplicationForPsychologist(newModel, applicationInDb.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.UpdateApplicationForPsychologist(newModel, applicationInDb.Id), Times.Never);
    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void DeleteApplicationForPsychologist_ValidRequestPassed_DeleteClient(string role)
    {
        //given
        var applicationInDb = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Roma",
            PhoneNumber = "89119856375",

            Client = new()
            {
                Id = 1,
                Email = "Va@gmail.com",
            }
        };

        if (role == Role.Manager.ToString())
        {
            applicationInDb.Client.Email = null;
        }
        _claims = new() { Email = applicationInDb.Client.Email, Role = role };

        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);
        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.DeleteApplicationsForPsychologist(applicationInDb.Id));

        //when
        _sut.DeleteApplicationsForPsychologist(applicationInDb.Id, _claims);

        //then
        var applications = _sut.GetAllApplicationsForPsychologist();
        Assert.Null(applications);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.DeleteApplicationsForPsychologist(It.IsAny<int>()), Times.Once);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void DeleteApplicationForPsychologist_EmptyRequest_ThrowEntityNotFoundException()
    {
        //given
        var testId = 1;
        _claims = new();
        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.DeleteApplicationsForPsychologist(testId));

        //when, then
        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.DeleteApplicationsForPsychologist(testId, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.DeleteApplicationsForPsychologist(It.IsAny<int>()), Times.Never);

    }

    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void DeleteApplicationForPsychologist_ClientGetSomeoneElseProfileAndRolePsychologist_ThrowAccessException(string role)
    {
        //given

        var applicationFirst = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Vasya",
            PhoneNumber = "89118888888",
            Client = new()
            {
                Id = 1,
                Email = "PPPPa@gmail.com",
            }
        };

        var applicationSecond = new ApplicationForPsychologistSearch()
        {
            Id = 2,
            Name = "Roma",
            PhoneNumber = "89119856375",
            Client = new()
            {
                Id = 2,
                Email = "Va@gmail.com",
            }
        };

        if (role == Role.Psychologist.ToString())
        {
            applicationFirst.Client.Email = applicationSecond.Client.Email;
        }

        _claims = new() { Email = applicationFirst.Client.Email, Role = role };
        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationSecond.Id)).Returns(applicationSecond);

        //when, then

        //when, then
        Assert.Throws<Exceptions.AccessException>(() => _sut.DeleteApplicationsForPsychologist(applicationSecond.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.DeleteApplicationsForPsychologist(It.IsAny<int>()), Times.Never);
    }
}