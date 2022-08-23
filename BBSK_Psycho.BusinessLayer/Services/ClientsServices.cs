using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.BusinessLayer.Services;

public class ClientsService : IClientsServices
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IClientsValidator _clientsValidator;

    public ClientsService(IClientsRepository clientsRepository,
                          IClientsValidator clientsValidator)
    {
        _clientsRepository = clientsRepository;
        _clientsValidator = clientsValidator;
    }

    public async Task<Client?> GetClientById(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await _clientsValidator.CheckAccess(claims, client);

        return client;
    }


    public async Task<List<Client>> GetClients()
    {
        var clients = await _clientsRepository.GetClients();

        return clients;
    }


    public async Task<List<Comment>> GetCommentsByClientId(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await _clientsValidator.CheckAccess(claims, client);

        return await _clientsRepository.GetCommentsByClientId(id);
    }


    public async Task<List<Order>> GetOrdersByClientId(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await _clientsValidator.CheckAccess(claims, client);

        return await _clientsRepository.GetOrdersByClientId(id);
    }

    public async Task<int> AddClient(Client client)
    {

         await _clientsValidator.CheckEmailForUniqueness(client.Email);

        client.Password = PasswordHash.HashPassword(client.Password);
        return await _clientsRepository.AddClient(client);
    }

    public async Task UpdateClient(Client newClientModel, int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await _clientsValidator.CheckAccess(claims, client);

        client.Name = newClientModel.Name;
        client.LastName = newClientModel.LastName;
        client.BirthDate = newClientModel.BirthDate;

        _clientsRepository.UpdateClient(client);
    }

    public async Task DeleteClient(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await _clientsValidator.CheckAccess(claims, client);

        await _clientsRepository.DeleteClient(client);
    }

    public async Task<List<ApplicationForPsychologistSearch>> GetApplicationsForPsychologistByClientId(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await _clientsValidator.CheckAccess(claims, client);

        return  client.ApplicationForPsychologistSearch.ToList();
    }
}