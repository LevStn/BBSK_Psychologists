using BBSK_Psycho.DataLayer.Entities;


namespace BBSK_Psycho.DataLayer.Repositories.Interfaces;

public interface IManagerRepository
{
    public Task<Manager?> GetManagerByEmail(string email);
}
