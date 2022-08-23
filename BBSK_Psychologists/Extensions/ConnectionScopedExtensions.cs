using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Helpers;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.BusinessLayer.Services.Validators;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.Extensions;

public static class ConnectionScopedExtensions
{

    public static void AddDataLayerRepositotories(this IServiceCollection services)
    {
        services.AddScoped<IClientsRepository, ClientsRepository>();
        services.AddScoped<IPsychologistsRepository, PsychologistsRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IApplicationForPsychologistSearchRepository, ApplicationForPsychologistSearchRepository>();
    }

    public static void AddBusinessLayerServices(this IServiceCollection services)
    {

        services.AddScoped<IClientsServices, ClientsService>();
        services.AddScoped<IPsychologistService, PsychologistService>();
        services.AddScoped<IPsychologistsValidator, PsychologistsValidator>();
        services.AddScoped<IAuthServices, AuthServices>();
        services.AddScoped<IClientsServices, ClientsService>();
        services.AddScoped<IApplicationForPsychologistSearchServices, ApplicationForPsychologistSearchServices>();
        services.AddScoped<ISearchByFilter, SearchByFilter>();
        services.AddScoped<IClientsValidator, ClientsValidator>();
        services.AddScoped<IApplicationsValidator, ApplicationsValidator>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IOrdersValidator, OrdersValidator>();
    }
}

