
using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IClientsServices
{
    public Task<int> AddClient(Client client);
    public Task DeleteClient(int id, ClaimModel claim);
    public Task<Client?> GetClientById(int id, ClaimModel claim);
    public Task<List<Client>> GetClients();
    public Task<List<Comment>> GetCommentsByClientId(int id, ClaimModel claim);
    public Task<List<Order>> GetOrdersByClientId(int id, ClaimModel claim);
    public Task UpdateClient(Client newClientModel, int id, ClaimModel claim);
    public Task<List<ApplicationForPsychologistSearch>> GetApplicationsForPsychologistByClientId(int id, ClaimModel claim);

}