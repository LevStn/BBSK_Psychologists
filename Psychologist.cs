namespace BBSK_Psycho
{
    public class Psychologist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Sex { get; set; }

        public DateTime BirthDate { get; set; } 

        public string Phone { get; set; }

        public string Email { get; set; }

        public int WorkExperience { get; set; }

        public  string PasportData { get; set; }

        public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"

        public int Status { get; set; }        //Enum

        public List<string>? TherapyMethods { get; set; }

        public List<string>? Problems { get; set; }

        public decimal Price { get; set; }  

        public Dictionary<DateTime, List <TimeOnly>> Schedule { get; set; }

        public string DenyMessage { get; set; }

    }
 
}