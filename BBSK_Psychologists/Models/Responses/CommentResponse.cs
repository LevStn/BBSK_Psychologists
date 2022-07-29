using System;
using BBSK_Psycho.Models.Responses;

namespace BBSK_Psycho.Models
{

    public class CommentResponse
    {
        public PsychologistResponse psychologistResponse { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; } 
        public DateTime Date { get; set; }
    }
}