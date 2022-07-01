using BBSK_Psycho.Enums;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests
{
    public class AddPsychologistRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
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

        [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequire)]
        [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLengthIsLessThanAllowed)]
        public string Password { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.EmailIsRequire)]
        public string Email { get; set; }
        [Required]
        public int WorkExperience { get; set; }
        [Required]
        public string PasportData { get; set; }
        [Required]
        public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"
        [Required]
        public  CheckStatus checkStatus{ get; set; }        //Enum
        [Required]
        public List<string>? TherapyMethods { get; set; }
        [Required]
        public List<string>? Problems { get; set; }
        [Required]
        public decimal Price { get; set; }
        //[Required]
        //public Dictionary<String, List<String>> Schedule { get; set; }

    }
}
