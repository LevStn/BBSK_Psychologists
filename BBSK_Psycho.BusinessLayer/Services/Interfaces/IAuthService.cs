

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IAuthService
{
    public ClaimModel GetUserForLogin(string email, string password);
}
