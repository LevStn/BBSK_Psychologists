using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly BBSK_PsychoContext _context;

    public ClientsRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public Client? GetClientById(int id) => _context.Clients.FirstOrDefault(c => c.Id == id && c.IsDeleted == false);

    public List<Client> GetClients() => _context.Clients.Where(c => c.IsDeleted== false).ToList();

    public List<Comment> GetCommentsByClientId(int id) => _context.Comments.Where(c => c.IsDeleted == false && c.Client.Id == id).ToList();

    public List<Order> GetOrdersByClientId(int id) => _context.Orders.Where(c => c.IsDeleted == false && c.Client.Id == id).ToList();

    public int AddClient(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();

        return client.Id;
    }

    public void UpdateClientById(Client client, int id)
    {
        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void DeleteClient(Client client)
    {
        client.IsDeleted = true;
        _context.SaveChanges();
    }

}
