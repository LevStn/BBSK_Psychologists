using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests;

public class FilterOptionRequest
{
    [Range(1, 2, ErrorMessage = ApiErrorMessage.RangeIsError)]
    public Price Price{ get; set; }

    [Required(ErrorMessage = ApiErrorMessage.NoProblemSelected)]
    public List<int> Problems { get; set; }

    [Range(1,2, ErrorMessage =ApiErrorMessage.RangeIsError)]
    public Gender? Gender { get; set; }
}
