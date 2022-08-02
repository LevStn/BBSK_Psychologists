using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BBSK_Psycho.DataLayer.Repositories;

public class ApplicationForPsychologistSearchRepository : IApplicationForPsychologistSearchRepository
{
    private readonly BBSK_PsychoContext _context;

    public ApplicationForPsychologistSearchRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public async Task<int> AddApplicationForPsychologist(ApplicationForPsychologistSearch request)
    {
        
        _context.ApplicationForPsychologistSearches.Add(request);
        await _context.SaveChangesAsync();

        return request.Id;
    }

    public async Task<List<ApplicationForPsychologistSearch>> GetAllApplicationsForPsychologist() => await _context.ApplicationForPsychologistSearches
       .Where(a => a.IsDeleted == false)
       .AsNoTracking()
       .ToListAsync();


    public async Task<ApplicationForPsychologistSearch?> GetApplicationForPsychologistById(int id) => await _context.ApplicationForPsychologistSearches
        .Include(a => a.Client)
        .FirstOrDefaultAsync(c => c.Id == id);


    public async Task DeleteApplicationForPsychologist(ApplicationForPsychologistSearch application)
    {
        application.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel)
    {

        _context.ApplicationForPsychologistSearches.Update(newModel);
        await _context.SaveChangesAsync();
    }
}
