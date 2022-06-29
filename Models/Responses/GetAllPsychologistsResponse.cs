namespace BBSK_Psycho.Models.Responses
{
    public class GetAllPsychologistsResponse
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Sex { get; set; }

        public int WorkExperience { get; set; }

        public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"

        public List<string>? TherapyMethods { get; set; }

        public List<string>? Problems { get; set; }

        public decimal Price { get; set; }

        public Dictionary<DateOnly, List<TimeOnly>> Schedule { get; set; }

    }
}

