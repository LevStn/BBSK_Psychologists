namespace BBSK_Psycho.Models;

public class RequestSearch
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Discription { get; set; }
    public string PsychologistGender { get; set; }
    public decimal CostMin { get; set; }
    public decimal CostMax { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
}
