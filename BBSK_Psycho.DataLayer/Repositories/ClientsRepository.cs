using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.DataLayer.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly BBSK_PsychoContext _context;

    public ClientsRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetClientById(int id) =>await _context.Clients
        .Include(c => c.ApplicationForPsychologistSearch.Where(a=>a.IsDeleted == false))
        .FirstOrDefaultAsync(c => c.Id == id);


    public async Task<List<Client>> GetClients() => await _context.Clients
        .Where(c => c.IsDeleted== false)
        .AsNoTracking()
        .ToListAsync();


    public async Task<List<Comment>> GetCommentsByClientId(int id) => await _context.Comments.Where(c => c.IsDeleted == false && c.Client.Id == id).ToListAsync();

    public async Task<List<Order>> GetOrdersByClientId(int id) => await _context.Orders.Where(c => c.IsDeleted == false && c.Client.Id == id).ToListAsync();

    public async Task<Client?> GetClientByEmail(string email) => await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<int> AddClient(Client client)
    {
        _context.Clients.Add(client);
       await _context.SaveChangesAsync();

        return client.Id;
    }

    public async Task UpdateClient(Client newModel)
    {
        _context.Clients.Update(newModel);
       await _context.SaveChangesAsync();
    }

    public async Task DeleteClient(Client client)
    {
        client.IsDeleted = true;
        _context.SaveChangesAsync();
    }

}
