using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.CustomAttributes;

public class ClientBirthDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime birthDate = DateTime.Parse(value.ToString());

        var today= DateTime.Today;
        var age = today.Year - birthDate.Year;

        var maxYoung = 15;
        var maxOld = 150;

        if (birthDate > DateTime.Now || age< maxYoung || age > maxOld)
        {
            return new ValidationResult(ApiErrorMessage.InvalidDate);
        }

        return null;
    }
}