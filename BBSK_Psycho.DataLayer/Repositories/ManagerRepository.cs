using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BBSK_Psycho.DataLayer.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly BBSK_PsychoContext _context;

    public ManagerRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public async Task<Manager?> GetManagerByEmail(string email) => await _context.Managers.FirstOrDefaultAsync(manager => manager.Email == email);
}
