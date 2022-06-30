using BBSK_Psycho.Enums;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Authorization;
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
        
        [AuthorizeByRole(Role.Manager)]
        [HttpGet("{id}")]
        public PsychologistResponse GetPsychologist(int id)
        {
            return new PsychologistResponse();
        }

        [AuthorizeByRole(Role.Client, Role.Psychologist)]
        [HttpGet()]
        public ActionResult <List<GetAllPsychologistsResponse>> GetAllPsychologists()
        {
            return new List<GetAllPsychologistsResponse>();
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPost()] 
        public ActionResult<int> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
        {
            return psychologistRequest.Id;
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPut("{id}")]
        public ActionResult<UpdatePsychologistResponse> UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
        {
            return new UpdatePsychologistResponse();
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpDelete("{id}")]
        public void DeletePsychologist(int id)
        {

        }

        [AuthorizeByRole(Role.Client, Role.Psychologist)]
        [HttpGet("{psychologistId}/comments")]
        public List <GetCommentsByPsychologistIdResponse> GetCommentsByPsychologistId(int psychologistId)
        {
            return new List <GetCommentsByPsychologistIdResponse>();
        }
    }
}
