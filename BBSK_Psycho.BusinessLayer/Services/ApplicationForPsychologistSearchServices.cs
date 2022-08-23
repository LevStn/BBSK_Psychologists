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
    private readonly IApplicationsValidator _applicationsValidator;
    private readonly IClientsRepository _clientsRepository;

    public ApplicationForPsychologistSearchServices(IApplicationForPsychologistSearchRepository applicationForPsychologistSearchRepository, 
                                                    IClientsRepository clientsRepository,
                                                    IApplicationsValidator applicationsValidator)
    {
        _applicationForPsychologistSearchRepository = applicationForPsychologistSearchRepository;
        _clientsRepository = clientsRepository;
        _applicationsValidator = applicationsValidator;
    }

    public async Task <int> AddApplicationForPsychologist(ApplicationForPsychologistSearch application, ClaimModel claim)
    {
        if ( claim is null)
        {
            throw new AccessException($"Access denied");
        }

        var client = await _clientsRepository.GetClientById(claim.Id);

        if (client == null)
        {
            throw new EntityNotFoundException($"Client {claim.Id} not found");
        }

        application.Client = client;

        return await _applicationForPsychologistSearchRepository.AddApplicationForPsychologist(application);
    }


    public async Task<List<ApplicationForPsychologistSearch>> GetAllApplicationsForPsychologist() => 
      await _applicationForPsychologistSearchRepository.GetAllApplicationsForPsychologist();


    public async Task<ApplicationForPsychologistSearch?> GetApplicationForPsychologistById(int id, ClaimModel claim)
    {
        var application = await _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application is null)
        {
            throw new EntityNotFoundException($"Application {id} not found");
        }

        await _applicationsValidator.CheckAccess(claim, application);

        return application;
    }


   
    public async Task DeleteApplicationForPsychologist(int id, ClaimModel claim)
    {
        var application = await _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application is null)
        {
            throw new EntityNotFoundException($"Application {id} not found");
        }

        await _applicationsValidator.CheckAccess(claim, application);

        _applicationForPsychologistSearchRepository.DeleteApplicationForPsychologist(application);
    }


    public async Task UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id, ClaimModel claim)
    {
        var application = await _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application is null)
        {
            throw new EntityNotFoundException($"Application {id} not found");
        }

        await _applicationsValidator.CheckAccess(claim, application);

        application.Name = newModel.Name;
        application.PhoneNumber = newModel.PhoneNumber;
        application.Description = newModel.Description;
        application.PsychologistGender = newModel.PsychologistGender;
        application.CostMin = newModel.CostMin;
        application.CostMax = newModel.CostMax;
        application.Date = newModel.Date;
        application.Time = newModel.Time;

        await _applicationForPsychologistSearchRepository.UpdateApplicationForPsychologist(application);
    }
}
