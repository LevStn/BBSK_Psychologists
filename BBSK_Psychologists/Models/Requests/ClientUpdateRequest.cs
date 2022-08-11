using System;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.CustomAttributes;

namespace BBSK_Psycho.Models;

public class ClientUpdateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string Name { get; set; }

    public string? LastName { get; set; }

    [ClientBirthDate]
    public DateTime? BirthDate { get; set; }

}