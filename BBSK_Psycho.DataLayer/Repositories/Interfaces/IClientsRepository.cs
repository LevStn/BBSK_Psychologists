using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories;

public interface IClientsRepository
{
    public Task<int> AddClient(Client client);
    public Task DeleteClient(Client client);
    public Task<Client?> GetClientById(int id);
    public Task<List<Client>> GetClients();
    public Task<List<Comment>> GetCommentsByClientId(int id);
    public Task<List<Order>> GetOrdersByClientId(int id);
    public Task UpdateClient(Client client);
    public Task<Client?> GetClientByEmail(string email);
}