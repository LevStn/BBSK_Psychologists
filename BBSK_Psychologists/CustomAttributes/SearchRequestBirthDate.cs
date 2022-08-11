using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.CustomAttributes
{
    public class SearchRequestBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthDate = DateTime.Parse(value.ToString());
            var maxYoung = 6;
            var maxOld = 150;

            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;


            if (birthDate > DateTime.Now || age < maxYoung || age > maxOld)
            {
                return new ValidationResult(ApiErrorMessage.InvalidDate);
            }

            return null;
        }
    }
}
