using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistsController : ControllerBase
    {
        private readonly ILogger<PsychologistsController> _logger;


        public PsychologistsController(ILogger<PsychologistsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Psychologist GetPsychologistById(int id)
        {
            return new Psychologist();
        }

        [HttpGet()]
        public  List<Psychologist> GetAllPsychologists()
        {
            var psychologists = new List<Psychologist>() { new Psychologist() { Name = "123" }, new Psychologist() { Name = "12aaaa3" }, new Psychologist() { Name = "68596" } };
            return psychologists;
        }

        [HttpPost()]
        public Psychologist AddPsychologist([FromBody] Psychologist psychologist)
        {
            psychologist.Id = 123;
            return psychologist;
        }

        [HttpPut("{id}")]
        public Psychologist UpdatePsychologistById([FromBody] Psychologist psychologist, int id)
        {
            var psychologistOld = new Psychologist();

            psychologistOld.Name = psychologist.Name;
            psychologistOld.Status = psychologist.Status;

            return psychologistOld;
        }

        [HttpDelete("{id}")]
        public void DeletePsychologistById(int id)
        {

        }

        [HttpGet("comments/{psychologistId}")]
        public List <Comment> GetCommentsById(int psychologistId)
        {
            var comments = new List<Comment>() {
                new Comment()
                {
                    PsychologistId = 1,Rating=100 , Text = "AALlaa"
                },
                new Comment()
                {
                    PsychologistId = 1,Rating=100 , Text = "AALlaa222"
                },
                new Comment()
                {
                    PsychologistId = 2,Rating=100 , Text = "AALlaa3333"
                },
                new Comment()
                {
                    PsychologistId = 2,Rating=100 , Text = "AALlaa4444"
                }
            };
            var result = comments.Where(c => c.PsychologistId == psychologistId).ToList();

            return result;
        }
    }
}
