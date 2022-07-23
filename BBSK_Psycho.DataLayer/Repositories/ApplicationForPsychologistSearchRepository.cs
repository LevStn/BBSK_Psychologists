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

    public int AddApplicationForPsychologist(ApplicationForPsychologistSearch application)
    {
        _context.ApplicationForPsychologistSearches.Add(application);
        _context.SaveChanges();

        return application.Id;
    }

    public List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist() => _context.ApplicationForPsychologistSearches
       .Where(a => a.IsDeleted == false)
       .AsNoTracking()
       .ToList();


    public ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id) => _context.ApplicationForPsychologistSearches.FirstOrDefault(c => c.Id == id);


    public List<ApplicationForPsychologistSearch> GetApplicationsForPsychologistByClientId(int id) => _context.ApplicationForPsychologistSearches.Where(c => c.Client.Id == id && c.IsDeleted == false).ToList();


    public void DeleteApplicationsForPsychologist(int id)
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
