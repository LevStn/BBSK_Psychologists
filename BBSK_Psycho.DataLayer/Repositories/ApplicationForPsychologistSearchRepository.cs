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

    public int AddApplicationForPsychologist(ApplicationForPsychologistSearch request, Client client)
    {
        request.Client = client;
        _context.ApplicationForPsychologistSearches.Add(request);
        
        _context.SaveChanges();

        return request.Id;
    }

    public List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist() => _context.ApplicationForPsychologistSearches
       .Where(a => a.IsDeleted == false)
       .AsNoTracking()
       .ToList();


    public ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id) => _context.ApplicationForPsychologistSearches
        .Include(a => a.Client)
        .FirstOrDefault(c => c.Id == id);


    public void DeleteApplicationForPsychologist(ApplicationForPsychologistSearch application)
    {
        application.IsDeleted = true;
        _context.SaveChanges();
    }

    public void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel)
    {

        _context.ApplicationForPsychologistSearches.Update(newModel);
        _context.SaveChanges();
    }
}
