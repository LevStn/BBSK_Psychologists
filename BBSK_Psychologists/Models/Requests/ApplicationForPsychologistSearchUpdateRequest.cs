using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Requests;

public class ApplicationForPsychologistSearchUpdateRequest
{

    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public Gender PsychologistGender { get; set; }
    public decimal CostMin { get; set; }
    public decimal CostMax { get; set; }
    public DateTime Date { get; set; }
    public TimeOfDay Time { get; set; }
}
