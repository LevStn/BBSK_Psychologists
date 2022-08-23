using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Validators
{
    public class ClientsValidator : IClientsValidator
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientsValidator(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task CheckAccess(ClaimModel claims, Client client)
        {
            if (!((claims.Email == client.Email
             || claims.Role == Role.Manager)
             && claims.Role != Role.Psychologist &&
             claims is not null))
                throw new AccessException($"Access denied");
        }

        public async Task CheckEmailForUniqueness(string email)
        {
            if (await _clientsRepository.GetClientByEmail(email) != null)
            {
                throw new UniquenessException($"That email is registred");
            }
        }
    }
}
