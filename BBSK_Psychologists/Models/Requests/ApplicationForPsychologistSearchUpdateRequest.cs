using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests;

public class ApplicationForPsychologistSearchUpdateRequest
{

    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    public string Name { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.PhoneNumberIsRequired)]
    public string PhoneNumber { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.DescriptionIsRequired)]
    public string Description { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.PsychologistGenderIsRequired)]
    public Gender PsychologistGender { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.CostMinIsRequired)]
    public decimal CostMin { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.CostMaxIsRequired)]
    public decimal CostMax { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.DateIsRequired)]
    public DateTime Date { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.TimeIsRequired)]
    public TimeOfDay Time { get; set; }

}
