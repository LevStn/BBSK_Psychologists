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
        public List<Psychologist> GetAllPsychologists()
        {
            return null!;
        }
        [HttpPost()]
        public Psychologist AddPsychologist()
        {
            return null!;
        }
        [HttpPatch("{id}")]
        public Psychologist UpdatePsychologistById()
        {
            return null!;
        }
        [HttpDelete("{}")]
        public Psychologist DeletePsychologist()
        {
            return null!;
        }
        [HttpGet("{id}")]
        public Psychologist GetCommentsById()
        {
            return null!;
        }
    }
}
