namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IPsychologistsValidator
    {
        Task CheckAccessForPsychologistManagersAndClients(int id, ClaimModel claim);
        Task CheckAccessOnlyForPsychologistAndManagers(int id, ClaimModel claim);
        Task CheckEmailForUniqueness(string email);
    }
}