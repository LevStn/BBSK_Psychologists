using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.CustomAttributes;

public class SearchRequestDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime sessionDate = DateTime.Parse(value.ToString());
        var today = DateTime.Today;

        if (sessionDate < DateTime.Now)
        {
            return new ValidationResult(ApiErrorMessage.InvalidSessionDate);
        }
        return null;
    }
}
