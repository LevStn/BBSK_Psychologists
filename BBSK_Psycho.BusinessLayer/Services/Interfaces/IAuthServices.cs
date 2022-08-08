

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IAuthServices
{
    public Task<ClaimModel> GetUserForLogin(string email, string password);
    public Task<string> GetToken(ClaimModel model);
}
