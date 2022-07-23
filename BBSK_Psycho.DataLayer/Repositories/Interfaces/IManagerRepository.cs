using BBSK_Psycho.DataLayer.Entities;


namespace BBSK_Psycho.DataLayer.Repositories.Interfaces;

public interface IManagerRepository
{
    public Manager? GetManagerByEmail(string email);
}
