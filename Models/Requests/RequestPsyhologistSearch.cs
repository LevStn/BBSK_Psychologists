using BBSK_Psycho.Enums;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class RequestPsyhologistSearch
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string Name { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.PhoneNumberIsRequired)]
    public string PhoneNumber { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.DescriptionIsRequired)]
    public string Description { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.PsychologistGenderIsRequired)]
    public Gender PsychologistGender { get; set; } // enum


    [Required(ErrorMessage = ApiErrorMessage.CostMinIsRequired)]
    public decimal CostMin { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.CostMaxIsRequired)]
    public decimal CostMax { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.DateIsRequired)]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.TimeIsRequired)]
    [DataType(DataType.Time)]
    public TimeOfDay Time { get; set; } // enum


    [Required(ErrorMessage = ApiErrorMessage.ClientIdIsRequired)]
    public int ClientId { get; set; }
}
