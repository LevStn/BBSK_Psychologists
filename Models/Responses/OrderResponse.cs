namespace BBSK_Psycho.Models;

public class OrderResponse
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int PsychologistId { get; set; }

    public decimal Cost { get; set; }

    public int Duration { get; set; }

    public string Message { get; set; } //Например, описание проблемы

    public string SessionDate { get; set; }

    public string OrderDate { get; set; }

    public string PayDate { get; set; }

    public int OrderStatus { get; set; } //Enum
}
