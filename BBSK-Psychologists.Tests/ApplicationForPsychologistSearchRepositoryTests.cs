

using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BBSK_Psychologists.Tests;

public class ApplicationForPsychologistSearchRepositoryTests
{
    private DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

    private ApplicationForPsychologistSearchRepository _sut;
    private BBSK_PsychoContext context;

    public ApplicationForPsychologistSearchRepositoryTests()
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

        _sut = new ApplicationForPsychologistSearchRepository(context);

    }

    [Test]
    public void AddApplicationForPsychologist_WhenCorrectData_ThenAddApplicationForPsychologistInDbAndReturnedId()
    {
        //given

        var client = new Client()
        {
            Name = "vasya",
            Email = "a@mail.ru",
            Password = "11111111",
            PhoneNumber = "89119802569",
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var application = new ApplicationForPsychologistSearch()
        {

            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day,
            Client= client
           

        };
       
        //when
        var actualId = _sut.AddApplicationForPsychologist(application, client.Id);
        context.SaveChanges();

        //then
        var actualdApplications = _sut.GetApplicationForPsychologistById(actualId);


        Assert.True(actualId == application.Id);
        Assert.True(actualdApplications.Name == application.Name);
        Assert.True(actualdApplications.PhoneNumber == application.PhoneNumber);
        Assert.True(actualdApplications.Description == application.Description);
        Assert.True(actualdApplications.PsychologistGender == application.PsychologistGender);
        Assert.True(actualdApplications.CostMin == application.CostMin);
        Assert.True(actualdApplications.CostMax == application.CostMax);
        Assert.True(actualdApplications.Date == application.Date);
        Assert.True(actualdApplications.Time == application.Time);
        Assert.False(actualdApplications.IsDeleted);
    }


    [Test]
    public void DeleteApplicationForPsychologist_WhenCorrecId_ThenSoftDelete()
    {
        //given
        var client = new Client()
        {
            Name = "vasya",
            Email = "a@mail.ru",
            Password = "11111111",
            PhoneNumber = "89119802569",
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var application = new ApplicationForPsychologistSearch()
        {
            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day,
            Client = client
            
        };
        context.ApplicationForPsychologistSearches.Add(application);
        context.SaveChanges();

        //when
        _sut.DeleteApplicationForPsychologist(application.Id);

        //then
        var actualdApplications = _sut.GetApplicationForPsychologistById(application.Id);

        Assert.True(actualdApplications.IsDeleted);
    }


    [Test]
    public void GetAllApplicationsForPsychologist_WhenCorrectDate_ThenApplicationsForPsychologistReturned()
    {
        //given
        var applicationFirst = new ApplicationForPsychologistSearch()
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
        var applicationSecond = new ApplicationForPsychologistSearch()
        {
            Name = "Petrovich",
            PhoneNumber = "89119850000",
            Description = "Hello",
            PsychologistGender = Gender.Male,
            CostMin = 1000,
            CostMax = 2000,
            Date = new DateTime(2022, 06, 02),
            Time = TimeOfDay.Morning
        };

        context.ApplicationForPsychologistSearches.Add(applicationFirst);
        context.ApplicationForPsychologistSearches.Add(applicationSecond);
        context.SaveChanges();

        //when
        var actualdApplications = _sut.GetAllApplicationsForPsychologist();

        //then
        Assert.True(actualdApplications is not null);
        Assert.True(actualdApplications.Count == 2);
        Assert.NotNull(actualdApplications.Find(a => a.Name == applicationFirst.Name && a.Id ==1));
        Assert.NotNull(actualdApplications.Find(a => a.Name == applicationSecond.Name && a.Id == 2));
        Assert.False(actualdApplications[0].IsDeleted);
        Assert.False(actualdApplications[1].IsDeleted);
    }

    [Test]
    public void GetApplicationForPsychologistById_WhenCorrectDataPassed_ThenApplicationReturned()
    {
        //given

        var client = new Client()
        {

            Name = "vasya",
            Email = "a@mail.ru",
            Password = "11111111",
            PhoneNumber = "89119802569",
            ApplicationForPsychologistSearch = new()
            {
                new()
                {
                    Name = "Alla",
                    PhoneNumber = "89119856375",
                    Description = "give me a help",
                    PsychologistGender = Gender.Male,
                    CostMin = 100,
                    CostMax = 200,
                    Date = new DateTime(2022, 02, 02),
                    Time = TimeOfDay.Day,
                }
            }
        };
        
        context.Clients.Add(client);
        context.SaveChanges();

        //when

        var actualdApplications = _sut.GetApplicationForPsychologistById(client.ApplicationForPsychologistSearch[0].Id);

        //then

        Assert.NotNull(actualdApplications);
        Assert.True(actualdApplications.Name == client.ApplicationForPsychologistSearch[0].Name);
        Assert.True(actualdApplications.PhoneNumber == client.ApplicationForPsychologistSearch[0].PhoneNumber);
        Assert.True(actualdApplications.Description == client.ApplicationForPsychologistSearch[0].Description);
        Assert.True(actualdApplications.PsychologistGender == client.ApplicationForPsychologistSearch[0].PsychologistGender);
        Assert.True(actualdApplications.CostMax == client.ApplicationForPsychologistSearch[0].CostMax);
        Assert.True(actualdApplications.CostMin == client.ApplicationForPsychologistSearch[0].CostMin);
        Assert.True(actualdApplications.Date == client.ApplicationForPsychologistSearch[0].Date);
        Assert.True(actualdApplications.Time == client.ApplicationForPsychologistSearch[0].Time);
        Assert.False(actualdApplications.IsDeleted);
    }


    


    [Test]
    public void UpdateApplicationsForPsychologist_WhenCorrectDate_ThenChangePoperties()
    {
        //given

        var client = new Client()
        {
            Name = "vasya",
            Email = "a@mail.ru",
            Password = "11111111",
            PhoneNumber = "89119802569",
        };

        context.Clients.Add(client);
        context.SaveChanges();

        ApplicationForPsychologistSearch dataForUpdate = new ApplicationForPsychologistSearch()
        {
            Id = 100,
            IsDeleted = true,
            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day,
        };



        var application = new ApplicationForPsychologistSearch()
        {

            Name = "Petya",
            PhoneNumber = "89119802536",
            Description = "Hello",
            PsychologistGender = Gender.Famale,
            CostMin = 20000,
            CostMax = 2555550,
            Date = new DateTime(2022, 01, 01),
            Time = TimeOfDay.Evening,
            Client = client

        };


        context.ApplicationForPsychologistSearches.Add(application);
        context.SaveChanges();

        //when
        _sut.UpdateApplicationForPsychologist(dataForUpdate, application.Id);

        //then
        var actualApplication = _sut.GetApplicationForPsychologistById(application.Id);

        Assert.True(actualApplication.Name == dataForUpdate.Name);
        Assert.True(actualApplication.PhoneNumber == dataForUpdate.PhoneNumber);
        Assert.True(actualApplication.Description == dataForUpdate.Description);
        Assert.True(actualApplication.PsychologistGender == dataForUpdate.PsychologistGender);
        Assert.True(actualApplication.CostMin == dataForUpdate.CostMin);
        Assert.True(actualApplication.CostMax == dataForUpdate.CostMax);
        Assert.True(actualApplication.Date == dataForUpdate.Date);
        Assert.True(actualApplication.Time == dataForUpdate.Time);
        Assert.True(actualApplication.Id != dataForUpdate.Id);
        Assert.True(actualApplication.IsDeleted != dataForUpdate.IsDeleted);

    }
}
