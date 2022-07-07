using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public interface IClientsRepository
    {
        int AddClient(Client client);
        void DeleteClientById(int id);
        Client? GetClientById(int id);
        List<Comment> GetCommentsByClientId(int id);
        List<Order> GetOrdersByClientId(int id);
        void UpdateClientById(Client client, int id);
    }
}