using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IClientsValidator
    {
        Task CheckAccess(ClaimModel claims, Client client);
        Task CheckEmailForUniqueness(string email);
    }
}