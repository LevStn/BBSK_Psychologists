using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;


namespace BBSK_Psycho.DataLayer.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly BBSK_PsychoContext _context;

    public ManagerRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public Manager? GetManagerByEmail(string email) => _context.Managers.FirstOrDefault(manager => manager.Email == email);
}
