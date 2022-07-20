

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IAuthServices
{
    public ClaimModel GetUserForLogin(string email, string password);
}
