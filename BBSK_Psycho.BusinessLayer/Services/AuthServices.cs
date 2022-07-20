

using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;

namespace BBSK_Psycho.BusinessLayer.Services;

public class AuthServices : IAuthServices
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IPsychologistsRepository _psychologistsRepository;

    public AuthServices(IClientsRepository clientsRepository, IPsychologistsRepository psychologistsRepository)
    {
        _clientsRepository = clientsRepository;
        _psychologistsRepository = psychologistsRepository;

    }

    

    public ClaimModel GetUserForLogin(string email, string password)
    {
        ClaimModel claimModel = new();

        if(email == "manager@p.ru" && password == "Manager777" )
        {
            claimModel.Email = email;
            claimModel.Role = Role.Manager.ToString();
            
        }
        else
        {
            var client = _clientsRepository.GetClientByEmail(email);
            var psychologist = _psychologistsRepository.GetPsychologistByEmail(email);

            if (client == null && psychologist == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            if (client != null)
            {
                if (client.Password == password)
                {

                    claimModel.Email = client.Email;
                    claimModel.Password = client.Password;
                    claimModel.Role = Role.Client.ToString();

                }
                else
                    throw new EntityNotFoundException("Invalid  password");

            }
            if (psychologist != null)
            {
                if (psychologist.Password == password)
                {

                    claimModel.Email = psychologist.Email;
                    claimModel.Password = psychologist.Password;
                    claimModel.Role = Role.Psychologist.ToString();
                }
                else
                    throw new EntityNotFoundException("Invalid  password");

            }

        }
        

        return claimModel;
    }
}
