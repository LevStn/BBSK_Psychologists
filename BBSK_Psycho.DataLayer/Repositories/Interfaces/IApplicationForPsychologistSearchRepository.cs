using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces;

public interface IApplicationForPsychologistSearchRepository
{
    public Task<int> AddApplicationForPsychologist(ApplicationForPsychologistSearch application);
    public Task DeleteApplicationForPsychologist(ApplicationForPsychologistSearch application);
    public Task<List<ApplicationForPsychologistSearch>> GetAllApplicationsForPsychologist();
    public Task<ApplicationForPsychologistSearch?> GetApplicationForPsychologistById(int id);
    public Task UpdateApplicationForPsychologist (ApplicationForPsychologistSearch newModel);
}