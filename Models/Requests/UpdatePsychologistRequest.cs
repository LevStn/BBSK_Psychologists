using BBSK_Psycho.Enums;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests
{
    public class UpdatePsychologistRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public Gender gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength (8)]
        public string Password { get; set; }
        [Required]
        public int WorkExperience { get; set; }
        [Required]
        public string PasportData { get; set; }
        [Required]
        public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"
        [Required]
        public CheckStatus checkStatus { get; set; }    //Enum
        [Required]
        public List<string>? TherapyMethods { get; set; }
        [Required]
        public List<string>? Problems { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Dictionary<DateTime, List<DateTime>> Schedule { get; set; }
    }
}
