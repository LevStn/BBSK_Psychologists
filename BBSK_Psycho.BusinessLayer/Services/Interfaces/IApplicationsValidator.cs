using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IApplicationsValidator
    {
        Task CheckAccess(ClaimModel claim, ApplicationForPsychologistSearch application);
    }
}