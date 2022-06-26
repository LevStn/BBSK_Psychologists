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
        public IActionResult GetPsychologistById(int id)
        {
            return Ok(new Psychologist());
        }

        [HttpGet()]
        public IActionResult GetAllPsychologists()
        {
            var psychologists = new List<Psychologist>() { new Psychologist() { Name = "123" }, new Psychologist() { Name = "12aaaa3" }, new Psychologist() { Name = "68596" } };
            return Ok(psychologists);
        }

        [HttpPost()]
        public IActionResult AddPsychologist([FromBody] Psychologist psychologist)
        {
            psychologist.Id = 123;
            return Ok(psychologist);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePsychologistById([FromBody] Psychologist psychologist, int id)
        {
            var psychologistOld = new Psychologist();

            psychologistOld.Name = psychologist.Name;
            psychologistOld.Status = psychologist.Status;

            return Ok(psychologistOld);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePsychologistById(int id)
        {

            return Ok();
        }

        [HttpGet("comments/{psychologistId}")]
        public IActionResult GetCommentsById(int psychologistId)
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

            return Ok(result);
        }
    }
}
