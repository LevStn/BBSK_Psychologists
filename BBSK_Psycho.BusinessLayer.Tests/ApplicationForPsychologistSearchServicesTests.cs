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
    private Mock<IApplicationForPsychologistSearchRepository> _applicationForPsychologistSearchRepositoryMock;
    private Mock<IClientsRepository> _clientsRepository;

    private ClaimModel _claims;

    [SetUp]
    public void Setup()
    {
        _applicationForPsychologistSearchRepositoryMock = new Mock<IApplicationForPsychologistSearchRepository>();
        _clientsRepository = new Mock<IClientsRepository>();
        _sut = new ApplicationForPsychologistSearchServices(_applicationForPsychologistSearchRepositoryMock.Object, _clientsRepository.Object);
    }


    [Test]
    public void AddApplicationForPsychologist_ValidRequestPassed_AddAplicationAndIdReturned()
    {
        //given
        var client = new Client()
        {
            Id = 1,
            Name = "Ad",
            Email = "ad@gmail.com",
            PhoneNumber = "89119802514",
            Password = "ada23qdq"
        };

        var application = new ApplicationForPsychologistSearch()
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
           
            
        };


        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.AddApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>()))
            .Returns(1);

        _clientsRepository.Setup(c => c.GetClientById(client.Id)).Returns(client);

        _claims = new() { Email = client.Email, Role = Role.Client, Id = client.Id };

        //when
        var actual = _sut.AddApplicationForPsychologist(application, _claims);

        //then
        Assert.AreEqual(actual, application.Id);
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
        Assert.AreEqual(actual.GetType(), typeof(List<ApplicationForPsychologistSearch>));
        Assert.True(actual[0].IsDeleted);
        Assert.False(actual[1].IsDeleted);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetAllApplicationsForPsychologist(), Times.Once());

    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void GetApplicationForPsychologistById_ValidRequestPassed_ApplicationsReceived(string roleString)
    {
        //given
        Role role = Enum.Parse<Role>(roleString);

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

        if (role == Role.Manager)
        {
            applicationInDb.Client.Email = null;
        }

        _claims = new() { Email = applicationInDb.Client.Email, Role = role };

        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

        //when

        var actual = _sut.GetApplicationForPsychologistById(applicationInDb.Id, _claims);

        //then

        Assert.AreEqual(actual.Id, applicationInDb.Id);
        Assert.AreEqual(actual.Client.Id, applicationInDb.Client.Id);
        Assert.False(actual.IsDeleted);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(applicationInDb.Id), Times.Once);

    }

    [Test]
    public void GetApplicationForPsychologistById_BadIdRequest_ThrowEntityNotFoundException()
    {
        //given
        var badId = 2;
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
        _claims = new() { Email = applicationInDb.Client.Email, Role = Role.Client};

        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);


        //when, then
        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetApplicationForPsychologistById(badId, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(badId), Times.Once);
    }


    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void GetApplicationForPsychologistById_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
    {
        //given
        Role role = Enum.Parse<Role>(roleString);
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

        if (role == Role.Psychologist)
        {
            testEmail = applicationInDb.Client.Email;
        }

        _claims = new() { Email = testEmail, Role = role };
        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

        //when, then
        Assert.Throws<Exceptions.AccessException>(() => _sut.GetApplicationForPsychologistById(applicationInDb.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(applicationInDb.Id), Times.Once);
    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void UpdateApplicationForPsychologist_ValidRequestPassed_ChangesProperties(string roleString)
    {
        //given
        Role role = Enum.Parse<Role>(roleString);

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
            Name = "Zara",
            PhoneNumber = "89119850000",
            Description = "Net",
            PsychologistGender = Gender.Famale,
            CostMin = 100000,
            CostMax = 20000000,
            Date = new DateTime(2022, 12, 30),
            Time = TimeOfDay.Morning,
        };

        if (role == Role.Manager)
        {
            applicationInDb.Client.Email = null;
        }
        _claims = new() { Email = applicationInDb.Client.Email, Role = role };


        _applicationForPsychologistSearchRepositoryMock.Setup(a => a.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);


        //when
        _sut.UpdateApplicationForPsychologist(newModel, applicationInDb.Id, _claims);

        //then

        var actual = _sut.GetApplicationForPsychologistById(applicationInDb.Id, _claims);

        _applicationForPsychologistSearchRepositoryMock.Verify(a => a.GetApplicationForPsychologistById(applicationInDb.Id), Times.Exactly(2));
        _applicationForPsychologistSearchRepositoryMock.Verify(a => a.UpdateApplicationForPsychologist(It.Is<ApplicationForPsychologistSearch>(a=>
        a.Name == newModel.Name &&
        a.PhoneNumber == newModel.PhoneNumber &&
        a.Description == newModel.Description &&
        a.PsychologistGender == newModel.PsychologistGender &&
        a.CostMin == newModel.CostMin && 
        a.CostMax == newModel.CostMax &&
        a.Date == newModel.Date &&
        a.Time == newModel.Time &&
        !a.IsDeleted)), Times.Once);
    }

    [Test]
    public void UpdateApplicationForPsychologist_EntityNotFound_ThrowEntityNotFoundException()
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

        _claims = new() { Email = applicationInDb.Client.Email, Role = Role.Client };

        //when, then

        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.UpdateApplicationForPsychologist(newModel, applicationInDb.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.UpdateApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>()), Times.Never);
    }

    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void UpdateApplicationForPsychologist_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
    {
        //given
        Role role = Enum.Parse<Role>(roleString);

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

        if (role == Role.Psychologist)
        {
            testEmail = applicationInDb.Client.Email;
        }
        _claims = new() { Email = testEmail, Role = role };

        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);

        //when, then

        Assert.Throws<Exceptions.AccessException>(() => _sut.UpdateApplicationForPsychologist(newModel, applicationInDb.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.UpdateApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>()), Times.Never);
    }


    [TestCase("Client")]
    [TestCase("Manager")]
    public void DeleteApplicationForPsychologist_ValidRequestPassed_DeleteClient(string roleString)
    {
        //given
        Role role = Enum.Parse<Role>(roleString);

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

        if (role == Role.Manager)
        {
            applicationInDb.Client.Email = null;
        }
        _claims = new() { Email = applicationInDb.Client.Email, Role = role };

        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationInDb.Id)).Returns(applicationInDb);


        //when
        _sut.DeleteApplicationForPsychologist(applicationInDb.Id, _claims);

        //then
        var applications = _sut.GetAllApplicationsForPsychologist();
        Assert.Null(applications);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.DeleteApplicationForPsychologist(applicationInDb), Times.Once);
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.GetApplicationForPsychologistById(applicationInDb.Id), Times.Once);
    }

    [Test]
    public void DeleteApplicationForPsychologist_RequestForNonexistentObject_ThrowEntityNotFoundException()
    {
        //given
        var testId = 1;
        _claims = new();

        //when, then
        Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.DeleteApplicationForPsychologist(testId, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.DeleteApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>()), Times.Never);

    }

    [TestCase("Client")]
    [TestCase("Psychologist")]
    public void DeleteApplicationForPsychologist_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
    {
        //given
        Role role = Enum.Parse<Role>(roleString);
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

        if (role == Role.Psychologist)
        {
            applicationFirst.Client.Email = applicationSecond.Client.Email;
        }

        _claims = new() { Email = applicationFirst.Client.Email, Role = role };
        _applicationForPsychologistSearchRepositoryMock.Setup(o => o.GetApplicationForPsychologistById(applicationSecond.Id)).Returns(applicationSecond);

        //when, then
        Assert.Throws<Exceptions.AccessException>(() => _sut.DeleteApplicationForPsychologist(applicationSecond.Id, _claims));
        _applicationForPsychologistSearchRepositoryMock.Verify(c => c.DeleteApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>()), Times.Never);
    }
}