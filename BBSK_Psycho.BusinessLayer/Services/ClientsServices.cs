using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;


namespace BBSK_Psycho.BusinessLayer.Services;

public class ClientsService : IClientsServices
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<Client?> GetClientById(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await CheckAccess(claims, client);

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

        await CheckAccess(claims, client);

        return await _clientsRepository.GetCommentsByClientId(id);
    }


    public async Task<List<Order>> GetOrdersByClientId(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await CheckAccess(claims, client);

        return await _clientsRepository.GetOrdersByClientId(id);
    }

    public async Task<int> AddClient(Client client)
    {
        await CheckEmailForUniqueness(client.Email);

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

        await CheckAccess(claims, client);

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

        await CheckAccess(claims, client);

        await _clientsRepository.DeleteClient(client);
    }

    public async Task<List<ApplicationForPsychologistSearch>> GetApplicationsForPsychologistByClientId(int id, ClaimModel claims)
    {
        var client = await _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        await CheckAccess(claims, client);

        return  client.ApplicationForPsychologistSearch.ToList();
    }


    private async Task CheckAccess(ClaimModel claims, Client client)
    {
        if ((!(((claims.Email == client.Email
         || claims.Role == Role.Manager)
         && claims.Role != Role.Psychologist) &&
         claims is not null)))
            throw new AccessException($"Access denied");
    }


    private async Task CheckEmailForUniqueness(string email) 
    {
        if( await _clientsRepository.GetClientByEmail(email) != null)
        {
            throw new UniquenessException($"That email is registred");
        }
    }
}