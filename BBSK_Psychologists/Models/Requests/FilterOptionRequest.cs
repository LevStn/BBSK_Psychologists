using BBSK_Psycho.CustomAttributes;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests;

public class FilterOptionRequest
{
    [Required(ErrorMessage = ApiErrorMessage.PriceIsRequired)]
    [EnumDataType(typeof(Price), ErrorMessage = ApiErrorMessage.RangeIsError))]
    public Price Price{ get; set; }

    [Required(ErrorMessage = ApiErrorMessage.NoProblemSelected)]
    public List<int> Problems { get; set; }

    [EnumDataType(typeof(Gender), ErrorMessage = ApiErrorMessage.RangeIsError)]
    public Gender? Gender { get; set; }
}
