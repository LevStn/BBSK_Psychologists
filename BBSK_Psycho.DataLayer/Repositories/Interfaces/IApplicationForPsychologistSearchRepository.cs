using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces;

public interface IApplicationForPsychologistSearchRepository
{
    int AddApplicationForPsychologist(ApplicationForPsychologistSearch application);
    void DeleteApplicationForPsychologist(ApplicationForPsychologistSearch application);
    List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist();
    ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id);
    void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel);
}