using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Validators
{
    public class PsychologistsValidator : IPsychologistsValidator
    {
        private readonly IPsychologistsRepository _psychologistsRepository;

        public PsychologistsValidator(IPsychologistsRepository psychologistsRepository)
        {
            _psychologistsRepository = psychologistsRepository;
        }

        public async Task CheckAccessOnlyForPsychologistAndManagers(int id, ClaimModel claim)
        {
            if (claim.Id != id
                && claim.Role != Role.Manager)
            {
                throw new AccessException($"Access denied");
            }
        }

        public async Task CheckAccessForPsychologistManagersAndClients(int id, ClaimModel claim)
        {
            if (claim.Role == Role.Psychologist
                && claim.Id != id)
            {
                throw new AccessException($"Access denied");
            }
        }

        public async Task CheckEmailForUniqueness(string email)
        {
            if (await _psychologistsRepository.GetPsychologistByEmail(email) != null)
            {
                throw new UniquenessException($"That email is registred");
            }
        }
    }
}
