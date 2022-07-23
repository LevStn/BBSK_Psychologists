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

    public int AddApplicationForPsychologist(ApplicationForPsychologistSearch request, int clientId)
    {
        var client = _context.Clients.FirstOrDefault(c=>c.Id == clientId);
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


    


    public void DeleteApplicationForPsychologist(int id)
    {
        var application = GetApplicationForPsychologistById(id);
        application.IsDeleted = true;
        _context.SaveChanges();
    }

    public void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id)
    {

        var application = GetApplicationForPsychologistById(id);

        application.Name = newModel.Name;
        application.PhoneNumber = newModel.PhoneNumber;
        application.Description = newModel.Description;
        application.PsychologistGender = newModel.PsychologistGender;
        application.CostMin = newModel.CostMin;
        application.CostMax = newModel.CostMax;
        application.Date = newModel.Date;
        application.Time = newModel.Time;


        _context.ApplicationForPsychologistSearches.Update(application);
        _context.SaveChanges();
    }
}
