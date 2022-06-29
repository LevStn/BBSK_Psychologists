namespace BBSK_Psycho.Models;

public class CommentResponse
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int PsychologistId { get; set; }     // PsyvhologistResponse

    public string Text { get; set; }

    public int Rating { get; set; }     // ќценка от 1 до 5 

    public DateTime Date { get; set; }
}
