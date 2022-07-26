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

    public int AddApplicationForPsychologist(ApplicationForPsychologistSearch application, ClaimModel claim)
    {
        if ( claim is null)
        {
            throw new AccessException($"Access denied");
        }

        var client = _clientsRepository.GetClientById(claim.Id);

        if (client == null)
        {
            throw new EntityNotFoundException($"Client {client.Id} not found");
        }
        
        return _applicationForPsychologistSearchRepository.AddApplicationForPsychologist(application, client);
    }

    public List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist() =>
        _applicationForPsychologistSearchRepository.GetAllApplicationsForPsychologist();




    public ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Application {id} not found");
        }

        if (CheckAccess(claim, application))
        {
            throw new AccessException($"Access denied");
        }
        else
            return application;
    }


   
    public void DeleteApplicationForPsychologist(int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (CheckAccess(claim, application))
        {
            throw new AccessException($"Access denied");
        }
        else
            _applicationForPsychologistSearchRepository.DeleteApplicationForPsychologist(application);
    }


    public void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Application {claim.Id} not found");
        }

        if (CheckAccess(claim, application))
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
            _applicationForPsychologistSearchRepository.UpdateApplicationForPsychologist(application);
        }

    }


    private bool CheckAccess(ClaimModel claim, ApplicationForPsychologistSearch application)
    {
        return (!(((claim.Email == application.Client.Email
            || claim.Role == Role.Manager)
            && claim.Role != Role.Psychologist) && claim is not null));
    }
}
