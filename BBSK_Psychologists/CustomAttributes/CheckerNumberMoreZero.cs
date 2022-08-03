using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.CustomAttributes;

public class CheckerNumberMoreZero : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        if (Convert.ToDecimal(value) <= 0)
        {
            return new ValidationResult(ApiErrorMessage.NumberLessOrEqualZero);

        }
        return null;
    }
}