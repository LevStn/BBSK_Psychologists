using System;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models
{

    public class ClientUpdateRequest
    {
        [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
        public string Name { get; set; }

        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }

    }
}