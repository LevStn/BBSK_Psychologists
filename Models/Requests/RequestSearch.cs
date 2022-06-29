namespace BBSK_Psycho.Models;

public class RequestSearch
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Description { get; set; }
    public string PsychologistGender { get; set; } // enum
    public decimal CostMin { get; set; }
    public decimal CostMax { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; } // enum
    public int ClientId { get; set; }
}
