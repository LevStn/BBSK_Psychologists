using System;

namespace BBSK_Psycho
{
    public class Comment // От клиента психологу
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int PsychologistId { get; set; }

        public int OrderId { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }     // Оценка от 1 до 5 

        public DateTime Date { get; set; }



    }
}
