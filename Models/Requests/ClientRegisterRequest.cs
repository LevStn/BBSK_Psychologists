namespace BBSK_Psycho.Models;

public class ClientRegisterRequest
{
    public string Name { get; set; }
    public string? LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
}
