using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.BusinessLayer.Services;

public class ApplicationForPsychologistSearchServices : IApplicationForPsychologistSearchServices
{

    private readonly IApplicationForPsychologistSearchRepository _applicationForPsychologistSearchRepository;
    private readonly IClientsRepository _clientsRepository;

    public ApplicationForPsychologistSearchServices(IApplicationForPsychologistSearchRepository applicationForPsychologistSearchRepository, IClientsRepository clientsRepository)
    {
        _applicationForPsychologistSearchRepository = applicationForPsychologistSearchRepository;
        _clientsRepository = clientsRepository;
    }

    public int AddApplicationForPsychologist(ApplicationForPsychologistSearch application)
    {
        return _applicationForPsychologistSearchRepository.AddApplicationForPsychologist(application);
    }

    public List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist() => _applicationForPsychologistSearchRepository.GetAllApplicationsForPsychologist();




    public ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claim.Email == application.Client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            return application;
    }


    public List<ApplicationForPsychologistSearch> GetApplicationsForPsychologistByClientId(int id, ClaimModel claim)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        if (!(((claim.Email == client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            return client.ApplicationForPsychologistSearch.ToList();
    }


    public void DeleteApplicationsForPsychologist(int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claim.Email == application.Client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            _applicationForPsychologistSearchRepository.DeleteApplicationsForPsychologist(id);
    }


    public void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claim.Email == application.Client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
        {
            application.Name = newModel.Name;
            application.PhoneNumber = newModel.PhoneNumber;
            application.Description = newModel.Description;
            application.PsychologistGender = newModel.PsychologistGender;
            application.CostMin = newModel.CostMin;
            application.CostMax = newModel.CostMax;
            application.Date = newModel.Date;
            application.Time = newModel.Time;
            _applicationForPsychologistSearchRepository.UpdateApplicationForPsychologist(newModel, id);
        }

    }

            
}
