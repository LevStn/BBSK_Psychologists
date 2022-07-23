using AutoMapper;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.Controllers;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BBSK_Psychologists.Tests;

public class ApplicationForPsychologistSearchControllerTests
{

    private ApplicationForPsychologistSearchController _sut;

    private Mock<IApplicationForPsychologistSearchServices> _applicationForPsychologistSearchMock;
    private Mock<IMapper> _mapper;

    private ClaimModel _claim;

    [SetUp]
    public void Setup()
    {
        _claim = new();
        _mapper = new Mock<IMapper>();
        _applicationForPsychologistSearchMock = new Mock<IApplicationForPsychologistSearchServices>();
        _sut = new ApplicationForPsychologistSearchController(_applicationForPsychologistSearchMock.Object, _mapper.Object);
    }

    [Test]
    public void AddApplicationForPsychologist_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        _applicationForPsychologistSearchMock.Setup(c => c.AddApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>(), It.IsAny<ClaimModel>()))
         .Returns(1);

        var request = new ApplicationForPsychologistSearchRequest()
        {
            Name = "Petro",
            Description = "H"
        };

        //when

        var actual = _sut.AddApplicationForPsychologist(request);


        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.True((int)actualResult.Value == 1);

        _applicationForPsychologistSearchMock.Verify(c => c.AddApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>(), It.IsAny<ClaimModel>()), Times.Once);

    }

    [Test]
    public void GetApplicationForPsychologistById_ValidRequestPassed_OkReceived()
    {
        //given
        var request = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Petro",
            Description = "H"
        };



        _applicationForPsychologistSearchMock.Setup(o => o.GetApplicationForPsychologistById(request.Id, _claim)).Returns(request);


        //when
        var actual = _sut.GetApplicationForPsychologistById(request.Id);

        //then
        var actualResult = actual.Result as ObjectResult;


        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        _applicationForPsychologistSearchMock.Verify(c => c.GetApplicationForPsychologistById(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);



    }

    //List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist();

 

    [Test]
    public void UpdateApplicationForPsychologist_ValidRequestPassed_NoContentReceived()
    {
        //given

        var request = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Petro",
            Description = "H"
        };


        var newModel = new ApplicationForPsychologistSearchUpdateRequest()
        {
            Name = "Vasya",
            Description = "Privet",
        };

        _applicationForPsychologistSearchMock.Setup(o => o.UpdateApplicationForPsychologist(request, request.Id, _claim));


        //when
        var actual = _sut.UpdateClientById(newModel, request.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _applicationForPsychologistSearchMock.Verify(c => c.UpdateApplicationForPsychologist(It.IsAny<ApplicationForPsychologistSearch>(), It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void DeleteApplicationForPsychologist_ValidRequestPassed_NoContentReceived()
    {
        //given
        var request = new ApplicationForPsychologistSearch()
        {
            Id = 1,
            Name = "Petro",
            Description = "H"
        };

        _applicationForPsychologistSearchMock.Setup(o => o.GetApplicationForPsychologistById(request.Id, _claim)).Returns(request);

        //when
        var actual = _sut.DeleteApplicationForPsychologist(request.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _applicationForPsychologistSearchMock.Verify(c => c.DeleteApplicationForPsychologist(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void GetAllApplicationsForPsychologist_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var requests = new List<ApplicationForPsychologistSearch>
        {
            new ApplicationForPsychologistSearch()
            {
                 Id = 1,
                 Name = "Petro",
                 Description = "H"
            },
            new ApplicationForPsychologistSearch()
            {
                Id = 2,
                Name = "Petro",
                Description = "H"
            },
            new ApplicationForPsychologistSearch()
            {
                Id = 3,
                Name = "Petro",
                Description = "H"
            }
        };


        _applicationForPsychologistSearchMock.Setup(o => o.GetAllApplicationsForPsychologist()).Returns(requests);

        //when
        var actual = _sut.GetAllApplicationsForPsychologist();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        _applicationForPsychologistSearchMock.Verify(c => c.GetAllApplicationsForPsychologist(), Times.Once);


    }



}
