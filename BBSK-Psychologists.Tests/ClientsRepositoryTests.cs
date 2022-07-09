using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;


namespace BBSK_Psychologists.Tests;
public class ClientsRepositoryTests
{
    private  DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

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
        
        if (context is not null)
             context.Database.EnsureDeleted();
        

        context = new BBSK_PsychoContext(_dbContextOptions);

        _sut = new ClientsRepository(context);

    }

    [Test]
    public void AddClient_WhenCorrectData_ThenAddClientInDbAndReturnId()
    {
        //given
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
        Assert.True(actualId == client.Id);
        


    }

    [Test]
    public void GetClientById_WhenCorrectDate_ThenReturnClient()
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
        var actualCLientFirs = _sut.GetClientById(client.Id);


        //then
        Assert.NotNull(actualCLientFirs);
        Assert.NotNull(actualCLientFirs.Name);
        Assert.NotNull(actualCLientFirs.LastName);
        Assert.NotNull(actualCLientFirs.Password);
        Assert.NotNull(actualCLientFirs.PhoneNumber);
        Assert.False(actualCLientFirs.IsDeleted);



    }

    [Test]
    public void GetCliens_WhenCorrectDate_ThenReturnClientsList()
    {
        //given
       var context = new BBSK_PsychoContext(_dbContextOptions);
       var _sut = new ClientsRepository(context);
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

    [Test]
    public void GetCommentsByClientId_WhenCorrecId_ThenReturnCommentsList()
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
       var actualComents= _sut.GetCommentsByClientId(expectedClient.Id);

        //then
        Assert.True(expectedClient.Comments.Count == actualComents.Count);
        Assert.True(actualComents[0].Text == "ApAp");
        Assert.True(actualComents[1].Text == "222");
        Assert.True(actualComents[0].Rating == 1);
        Assert.True(actualComents[1].Rating == 3);
        Assert.NotNull(actualComents[0].Date);
        Assert.NotNull(actualComents[1].Date);

    }

    [Test]
    public void GetOrdersByClientId_WhenCorrecId_ThenReturnOrdersList()
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
        var actualOrders = _sut.GetOrdersByClientId(expectedClient.Id);

        //then
        Assert.True(expectedClient.Orders.Count == actualOrders.Count);
        Assert.True(actualOrders[0].Message == "ApAp");
        Assert.True(actualOrders[1].Message == "222");
        Assert.True(actualOrders[0].Cost == 1);
        Assert.True(actualOrders[1].Cost == 3);
        Assert.NotNull(actualOrders[0].PayDate);
        Assert.NotNull(actualOrders[1].PayDate);

    }


}

