using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psychologists.Tests;


public class ClientsControllerTests
{

    private readonly DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

    private ClientsRepository repository;
    private BBSK_PsychoContext context;
    private ClientsController _sut;
    public ClientsControllerTests()
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
        repository = new ClientsRepository(context);
        _sut = new ClientsController(repository);

    }

    [Test]
    public void AddClient_ValidRequestPassed_CreatedResultReceived()
    {

        //given
        var expectedId = 1;
        var client = new ClientRegisterRequest()
        {
            Name = "Petro",
            Password = "1234567894",
            Email = "a@hdjk.com"
        };
        //when
        var actual = _sut.AddClient(client);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);
        _sut.DeleteClientById(actual.Value);
    }



    [Test]
    public void GetClientById_ValidRequestPassed_OkReceived()
    {
        //given
        var client = new Client()
        {

            Name = "Roma",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };

        context.Clients.Add(client);
        context.SaveChanges();

        //when
        var actual = _sut.GetClientById(client.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
      
    }

    [Test]
    public void UpdateClientById_ValidRequestPassed_NoContentReceived()
    {
        //given

        var client = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };

        var newProperty = new ClientUpdateRequest()
        {
            Name = "Petro",
            LastName = "Sobakov",
        };


        context.Clients.Add(client);
        context.SaveChanges();

        //when
        var actual = _sut.UpdateClientById(newProperty, client.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

    }

    [Test]
    public void GetCommentsByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var expectedClient = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            Comments = new()
            {
                new()
                {
                    Id = 1, Text="ApAp",Rating=1,Date=DateTime.Now
                },
                new()
                {
                    Id = 2, Text="222",Rating=3,Date=DateTime.Now
                }
            },

        };


        context.Clients.Add(expectedClient);
        context.SaveChanges();

        //when
        var actual = _sut.GetCommentsByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
    }


    [Test]
    public void GetOrdersByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        //given

        var expectedClient = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            Orders = new()
            {
                new()
                {
                    Id = 1, Message="ApAp",Cost=1,PayDate=DateTime.Now
                },
                new()
                {
                    Id = 2, Message="222",Cost=3,PayDate=DateTime.Now
                }
            },

        };


        context.Clients.Add(expectedClient);
        context.SaveChanges();

        //when
        var actual = _sut.GetOrdersByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
    }

    [Test]
    public void DeleteClientById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var client = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",

        };


        context.Clients.Add(client);
        context.SaveChanges();

        //when
        var actual = _sut.DeleteClientById(client.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

    }

    [Test]
    public void GetClients_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var clientFirs = new Client()
        {

            Name = "John",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };
        var clientSecond = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            IsDeleted = true,
        };
        var clientThird = new Client()
        {

            Name = "Petya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };

        context.Clients.Add(clientFirs);
        context.Clients.Add(clientSecond);
        context.Clients.Add(clientThird);
        context.SaveChanges();

        //when
        var actual = _sut.GetClients();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

    }

}

