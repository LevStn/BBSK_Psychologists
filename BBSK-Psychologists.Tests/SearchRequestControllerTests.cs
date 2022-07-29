using AutoMapper;
using BBSK_Psycho;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.Controllers;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BBSK_Psychologists.Tests;

public class SearchRequestControllerTests
{

    private SearchRequestsController _sut;

    private Mock<IApplicationForPsychologistSearchServices> _applicationForPsychologistSearchMock;

    private IMapper _mapper;

    private ClaimModel _claim;

    [SetUp]
    public void Setup()
    {
        _claim = new();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigStorage>()));
        _applicationForPsychologistSearchMock = new Mock<IApplicationForPsychologistSearchServices>();
        _sut = new SearchRequestsController(_applicationForPsychologistSearchMock.Object, _mapper);
    }

    [Test]
    public void AddApplicationForPsychologist_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        _applicationForPsychologistSearchMock.Setup(c => c.AddApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>(), It.IsAny<ClaimModel>()))
         .Returns(1);
        var expectedId = 1;
        var request = new SearchRequest()
        {
            Name = "Petro",
            PhoneNumber = "89118002695",
            Description = "H",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = DateTime.Now,
            Time= TimeOfDay.Morning

        };


        //when
        var actual = _sut.AddApplicationForPsychologist(request);


        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.AreEqual((int)actualResult.Value, expectedId);

        _applicationForPsychologistSearchMock.Verify(c => c.AddApplicationForPsychologist(It.Is<ApplicationForPsychologistSearch>(a=>
        a.Name == request.Name &&
        a.PhoneNumber == request.PhoneNumber &&
        a.Description == request.Description &&
        a.PsychologistGender == request.PsychologistGender &&
        a.CostMin == request.CostMin &&
        a.CostMax == request.CostMax &&
        a.Date == request.Date &&
        a.Time == request.Time), It.IsAny<ClaimModel>()), Times.Once);

    }

    [Test]
    public void GetApplicationForPsychologistById_ValidRequestPassed_OkReceived()
    {
        //given
        var applicationForPsychologist = new ApplicationForPsychologistSearch()
        {
            Id =1,
            Name = "Petro",
            PhoneNumber = "89118002695",
            Description = "H",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = DateTime.Now,
            Time = TimeOfDay.Morning,
        };

        _applicationForPsychologistSearchMock.Setup(a => a.GetApplicationForPsychologistById(applicationForPsychologist.Id, It.IsAny<ClaimModel>())).Returns(applicationForPsychologist);


        //when
        var actual = _sut.GetApplicationForPsychologistById(applicationForPsychologist.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var actualRequest = actualResult.Value as SearchResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        Assert.AreEqual(applicationForPsychologist.Id, actualRequest.Id);
        Assert.AreEqual(applicationForPsychologist.Name, actualRequest.Name);
        Assert.AreEqual(applicationForPsychologist.PhoneNumber, actualRequest.PhoneNumber);
        Assert.AreEqual(applicationForPsychologist.Description, actualRequest.Description);
        Assert.AreEqual(applicationForPsychologist.PsychologistGender, actualRequest.PsychologistGender);
        Assert.AreEqual(applicationForPsychologist.CostMin, actualRequest.CostMin);
        Assert.AreEqual(applicationForPsychologist.CostMax, actualRequest.CostMax);
        Assert.AreEqual(applicationForPsychologist.Date, actualRequest.Date);
        Assert.AreEqual(applicationForPsychologist.Time, actualRequest.Time);

        _applicationForPsychologistSearchMock.Verify(a => a.GetApplicationForPsychologistById(applicationForPsychologist.Id, It.IsAny<ClaimModel>()), Times.Once);

    }


    [Test]
    public void UpdateApplicationForPsychologist_ValidRequestPassed_NoContentReceived()
    {
        //given

        var applicationForPsychologist = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Petro",
            PhoneNumber = "89118002695",
            Description = "H",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = DateTime.Now,
            Time = TimeOfDay.Morning,
        };


        var newModel = new SearchRequest()
        {
           
            Name = "Cat",
            PhoneNumber = "89118000000",
            Description = "HI",
            PsychologistGender = Gender.Famale,
            CostMin = 1000,
            CostMax = 2000,
            Date = DateTime.Now,
            Time = TimeOfDay.Evening,
        };

        _applicationForPsychologistSearchMock.Setup(a => a.UpdateApplicationForPsychologist(applicationForPsychologist, applicationForPsychologist.Id, _claim));


        //when
        var actual = _sut.UpdateClientById(newModel, applicationForPsychologist.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _applicationForPsychologistSearchMock.Verify(a => a.UpdateApplicationForPsychologist(It.Is<ApplicationForPsychologistSearch>(a=>
        a.Name == newModel.Name &&
        a.PhoneNumber == newModel.PhoneNumber &&
        a.Description == newModel.Description &&
        a.PsychologistGender == newModel.PsychologistGender &&
        a.CostMin == newModel.CostMin &&
        a.CostMax == newModel.CostMax &&
        a.Date == newModel.Date &&
        a.Time == newModel.Time &&
        !a.IsDeleted), applicationForPsychologist.Id, It.IsAny<ClaimModel>()), Times.Once);
    }

    [Test]
    public void DeleteApplicationForPsychologist_ValidRequestPassed_NoContentReceived()
    {
        //given
        var applicationForPsychologist = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Petro",
            PhoneNumber = "89118002695",
            Description = "H",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = DateTime.Now,
            Time = TimeOfDay.Morning,
        };

        _applicationForPsychologistSearchMock.Setup(a => a.GetApplicationForPsychologistById(applicationForPsychologist.Id, _claim)).Returns(applicationForPsychologist);

        //when
        var actual = _sut.DeleteApplicationForPsychologist(applicationForPsychologist.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _applicationForPsychologistSearchMock.Verify(a => a.DeleteApplicationForPsychologist(applicationForPsychologist.Id, It.IsAny<ClaimModel>()), Times.Once);
    }


    [Test]
    public void GetAllApplicationsForPsychologist_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var applicationForPsychologist = new List<ApplicationForPsychologistSearch>
        {
            new ApplicationForPsychologistSearch()
            {
                 Id = 1,
                 Name = "Petro",
                 PhoneNumber = "89118002695",
                 Description = "H",
                 PsychologistGender = Gender.Male,
                 CostMin = 100,
                 CostMax = 200,
                 Date = DateTime.Now,
                 Time = TimeOfDay.Morning,
            },
            new ApplicationForPsychologistSearch()
            {
                 Id = 2,
                 Name = "Vas",
                 PhoneNumber = "89118002600",
                 Description = "Hi",
   
            },
            new ApplicationForPsychologistSearch()
            {
                Id = 3,
                Name = "Petro",
                Description = "H"
            }
        };

        _applicationForPsychologistSearchMock.Setup(o => o.GetAllApplicationsForPsychologist()).Returns(applicationForPsychologist);

        //when
        var actual = _sut.GetAllApplicationsForPsychologist();

        //then
        var actualResult = actual.Result as ObjectResult;
        var actualRequest = actualResult.Value as List<SearchResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(actualRequest.Count, applicationForPsychologist.Count);
        Assert.AreEqual(actualRequest[0].Id, applicationForPsychologist[0].Id);
        Assert.AreEqual(actualRequest[0].Name, applicationForPsychologist[0].Name);
        Assert.AreEqual(actualRequest[0].PhoneNumber, applicationForPsychologist[0].PhoneNumber);
        Assert.AreEqual(actualRequest[0].Description, applicationForPsychologist[0].Description);
        Assert.AreEqual(actualRequest[0].PsychologistGender, applicationForPsychologist[0].PsychologistGender);
        Assert.AreEqual(actualRequest[0].CostMin, applicationForPsychologist[0].CostMin);
        Assert.AreEqual(actualRequest[0].CostMax, applicationForPsychologist[0].CostMax);
        Assert.AreEqual(actualRequest[0].Date, applicationForPsychologist[0].Date);
        Assert.AreEqual(actualRequest[0].Time, applicationForPsychologist[0].Time);
        Assert.AreEqual(actualRequest[1].Id, applicationForPsychologist[1].Id);
        Assert.AreEqual(actualRequest[2].Id, applicationForPsychologist[2].Id);


        _applicationForPsychologistSearchMock.Verify(a => a.GetAllApplicationsForPsychologist(), Times.Once);

    }
}
