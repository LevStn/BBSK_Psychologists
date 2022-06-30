using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class ClientRegisterRequest
{
    [Required(ErrorMessage = ApiErrorMessage.ClientNameIsRequired)]
    public string Name { get; set; }

    public string? LastName { get; set; }
    
    [Required(ErrorMessage = ApiErrorMessage.PasswordLengthIsLessThanAllowed)]
    [MinLength(8)]
    public string Password { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.InvalidCharacterInEmail)]
    [EmailAddress]
    public string Email { get; set; }
    
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
}
