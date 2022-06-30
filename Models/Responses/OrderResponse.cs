using BBSK_Psycho.Models.Responses;

namespace BBSK_Psycho.Models;

public class OrderResponse
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public PsychologistResponse psychologistResponse { get; set; }

    public decimal Cost { get; set; }

    public int Duration { get; set; }

    public string Message { get; set; } //Например, описание проблемы

    public DateTime SessionDate { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime PayDate { get; set; }

    public int OrderStatus { get; set; } //Enum
}
