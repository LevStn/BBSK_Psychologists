
namespace BBSK_Psycho.DataLayer.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string EducationData { get; set; }
        public bool IsDelete { get; set; }


        public Psychologist Psychologist { get; set; }
    }
}
