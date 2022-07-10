using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using Moq;

namespace BBSK_Psychologists.Tests;


public class ClientsControllerTests
{


    public ClientsController _sut;
    private Mock<IClientsRepository> _clientsRepositoryMock;
    

    [SetUp]
    public void Setup()
    {
        _clientsRepositoryMock = new Mock<IClientsRepository>();
        _sut= new ClientsController(_clientsRepositoryMock.Object);
        
    }

    [Test]
    public void AddClient_ValidRequestPassed_CreatedResultReceived()
    {
        //given

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

    }



    [Test]
    public void GetClientById_ValidRequestPassed_OkReceived()
    {
        //given
        var expectedClient = new Client()
        {
            Id = 1,
            Name = "Roma",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };
        _clientsRepositoryMock.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient).Verifiable();


        //when
        var actual = _sut.GetClientById(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(actualResult.Value, expectedClient);

    }

    [Test]
    public void UpdateClientById_ValidRequestPassed_NoContentReceived()
    {
        //given

        var client = new Client()
        {
            Id=1,
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

        _clientsRepositoryMock.Setup(o => o.UpdateClient(client, client.Id)).Verifiable();


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
            Id = 1,
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


        _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(expectedClient.Id)).Returns(expectedClient.Comments).Verifiable();

        //when
        var actual = _sut.GetCommentsByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedClient.Comments, actualResult.Value);
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

        _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(expectedClient.Id)).Returns(expectedClient.Orders).Verifiable();

        //when
        var actual = _sut.GetOrdersByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedClient.Orders, actualResult.Value);
    }

    [Test]
    public void DeleteClientById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var expectedClient = new Client()
        {
            Id=1,
            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            IsDeleted = false

        };

        _clientsRepositoryMock.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient).Verifiable();

        //when
        var actual = _sut.DeleteClientById(expectedClient.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

    }

    [Test]
    public void GetClients_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var clients = new List<Client>
        {
            new Client()
            {
                Name = "John",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
            },
            new Client()
            {
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = true,
            },
            new Client()
            {
                 Name = "Petya",
                 LastName = "Petrov",
                 Email = "Va@gmail.com",
                 Password = "12345678dad",
                 PhoneNumber = "89119856375",
            }
        };
        

        _clientsRepositoryMock.Setup(o => o.GetClients()).Returns(clients).Verifiable();

        //when
        var actual = _sut.GetClients();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

    }

}

