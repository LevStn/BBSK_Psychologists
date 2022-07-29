using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models
{
    public class SearchRequest
    {
        [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.PhoneNumberIsRequired)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.DescriptionIsRequired)]
        public string Description { get; set; }

        public Gender PsychologistGender { get; set; } 

        [Required(ErrorMessage = ApiErrorMessage.CostMinIsRequired)]
        public decimal CostMin { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.CostMaxIsRequired)]
        public decimal CostMax { get; set; }

        public DateTime Date { get; set; }

        public TimeOfDay Time { get; set; } 


    }
}