using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;


namespace BBSK_Psychologists.Tests;
public class ClientsRepositoryTests
{
    private readonly DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

    private ClientsRepository _sut;
    private BBSK_PsychoContext context;

    public ClientsRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
      
    }

    [SetUp]
    public void Setup()
    {
        context = new BBSK_PsychoContext(_dbContextOptions);
        _sut = new ClientsRepository(context);

    }

    [Test]
    public void AddClient_WhenCorrectData_ThenAddClientInDbAndReturnId()
    {
        //given

        var expectedId = 1;
        var client = new Client()
        {

            Name = "Alla",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
         };

        //when
        var actualId=_sut.AddClient(client);
        context.SaveChanges();

        //then
        Assert.True(client.RegistrationDate< DateTime.Now);
        Assert.False(client.IsDeleted);
        Assert.NotNull(client.RegistrationDate);
        Assert.True(actualId == expectedId);
        _sut.DeleteClient(actualId);


    }

    [Test]
    public void GetClientById_WhenCorrectDate_ThenReturnClient()
    {
        //given

        var clientFirs = new Client()
        {

            Name = "Roma",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };
        var clientSecond = new Client()
        {

            Name = "Barbara",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            IsDeleted = true,
        };

        context.Clients.Add(clientFirs);
        context.Clients.Add(clientSecond);
        context.SaveChanges();

        //when
        var actualCLientFirs = _sut.GetClientById(clientFirs.Id);
        var actualCLientSecond = _sut.GetClientById(clientSecond.Id);

        //then
        Assert.NotNull(actualCLientFirs);
        Assert.NotNull(actualCLientSecond);
        Assert.NotNull(actualCLientFirs.Name);
        Assert.NotNull(actualCLientFirs.LastName);
        Assert.NotNull(actualCLientFirs.Password);
        Assert.NotNull(actualCLientFirs.PhoneNumber);
        Assert.False(actualCLientFirs.IsDeleted);
        Assert.True(actualCLientSecond.IsDeleted);


    }

    [Test]
    public void GetCliens_WhenCorrectDate_ThenReturnClientsList()
    {
        //given

        var expectedCount = 2;
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
        var actualCLient = _sut.GetClients();

        //then
        Assert.NotNull(actualCLient);
        Assert.True (actualCLient.GetType() == typeof (List<Client>));
        Assert.True(actualCLient.Count == expectedCount);

    }


    [Test]
    public void UpdateClient_WhenCorrectDate_ThenChangingPoperties()
    {
        //given

        var expectedId = 1;

        Client expectedClient = new Client()
        {
            Id = 10,
            Name = "Alex",
            LastName = "Alen",
            Email = "AAAAA@gmail.com",
            Password = "000000000",
            PhoneNumber = "0000",
            BirthDate = null,
        };



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
        _sut.UpdateClient(expectedClient, client.Id);

        //then
        Assert.True(expectedClient.Id != client.Id);
        Assert.True(client.Name == expectedClient.Name);
        Assert.True(client.LastName == expectedClient.LastName);
        Assert.True(client.BirthDate == expectedClient.BirthDate);
        Assert.True(client.Email != expectedClient.Email);
        Assert.True(client.Password != expectedClient.Password);
        Assert.True(client.PhoneNumber != expectedClient.PhoneNumber);
 
    }

    public void DeleteClient_WhenCorrecId_ThenSoftDelete()
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
        _sut.DeleteClient(client.Id);

        //then
        Assert.True(client.IsDeleted);
        Assert.NotNull(client.Id);
        Assert.NotNull(client.Name);
        Assert.NotNull(client.Email);
        Assert.NotNull(client.Password);
        Assert.NotNull(client.PhoneNumber);

    }


}

