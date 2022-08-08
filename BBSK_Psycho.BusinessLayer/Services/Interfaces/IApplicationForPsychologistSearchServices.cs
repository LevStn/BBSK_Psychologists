using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IApplicationForPsychologistSearchServices
{
    public  Task<int> AddApplicationForPsychologist(ApplicationForPsychologistSearch application, ClaimModel claim);
    public Task DeleteApplicationForPsychologist(int id, ClaimModel claim);
    public Task<List<ApplicationForPsychologistSearch>> GetAllApplicationsForPsychologist();
    public Task<ApplicationForPsychologistSearch?> GetApplicationForPsychologistById(int id, ClaimModel claim);
    public Task UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id, ClaimModel claim);
}