using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Validators
{
    public class ApplicationsValidator : IApplicationsValidator
    {
        public async Task CheckAccess(ClaimModel claim, ApplicationForPsychologistSearch application)
        {
            if (!(((claim.Email == application.Client.Email
               || claim.Role == Role.Manager)
               && claim.Role != Role.Psychologist) &&
               claim is not null))
                throw new AccessException($"Access denied");
        }
    }
}
