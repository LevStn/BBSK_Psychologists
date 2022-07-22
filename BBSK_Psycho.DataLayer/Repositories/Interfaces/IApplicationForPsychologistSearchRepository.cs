using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces;

public interface IApplicationForPsychologistSearchRepository
{
    int AddApplicationForPsychologist(ApplicationForPsychologistSearch application);
    void DeleteApplicationsForPsychologist(int id);
    List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist();
    ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id);
    List<ApplicationForPsychologistSearch> GetApplicationsForPsychologistByClientId(int id);
    void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id);
}