using BBSK_Psycho.Enums;

namespace BBSK_Psycho
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int PsychologistId { get; set; }

        public decimal Cost { get; set; }

        public SessionDuration Duration { get; set; }

        public string Message { get; set; } //Например, описание проблемы

        public DateTime SessionDate { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime PayDate { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
