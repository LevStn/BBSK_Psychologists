using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class Loginrequest
{
    [Required(ErrorMessage = ApiErrorMessage.InvalidCharacterInEmail)]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequire)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLengthIsLessThanAllowed)]
    public string Password { get; set; }
}
