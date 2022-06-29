namespace BBSK_Psycho.Models.Requests
{
    public class OrderCreateRequest
    {
        public int ClientId { get; set; }

        public int PsychologistId { get; set; }

        public decimal Cost { get; set; }

        public int Duration { get; set; }

        public string Message { get; set; }

        public DateTime SessionDate { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime PayDate { get; set; }

        public int OrderStatus { get; set; }
    }
}
