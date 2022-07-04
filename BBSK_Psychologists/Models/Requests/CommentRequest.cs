using BBSK_Psycho.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests
{
    public class CommentRequest
    {
        [Required(ErrorMessage = ApiErrorMessage.ClientIdIsRequired)]
        public int ClientId { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.PsychologistIdIsRequired)]
        public int PsychologistId { get; set; }

        public string Text { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.RatingIsRequired)]
        public int Rating { get; set; }     // Оценка от 1 до 5 

        [Required(ErrorMessage = ApiErrorMessage.DateIsRequired)]
        public DateTime Date { get; set; }

    }
}
