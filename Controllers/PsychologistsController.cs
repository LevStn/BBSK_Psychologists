using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;
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
        public PsychologistResponse GetPsychologist(int id)
        {
            return new PsychologistResponse();
        }

        [HttpGet()]
        public  List<GetAllPsychologistsResponse> GetAllPsychologists()
        {
            return new List<GetAllPsychologistsResponse>();
        }

        [HttpPost()] 
        public ActionResult<int> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
        {
            return psychologistRequest.Id;
        }

        [HttpPut("{id}")]
        public UpdatePsychologistResponse UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
        {
            return new UpdatePsychologistResponse();
        }

        [HttpDelete("{id}")]
        public void DeletePsychologist(int id)
        {

        }

        // /Psychologists/{psychologistId}/comments
        [HttpGet("{psychologistId}/comments")]
        public List <GetCommentsByPsychologistIdResponse> GetCommentsByPsychologistId(int psychologistId)
        {
            return new List <GetCommentsByPsychologistIdResponse>();
        }
    }
}
