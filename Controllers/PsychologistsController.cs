using BBSK_Psycho.Enums;
using BBSK_Psycho.Extensions;
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
        [ProducesResponseType(typeof(PsychologistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PsychologistResponse> GetPsychologist(int id)
        {
            return Ok(new PsychologistResponse());
        }

        [AuthorizeByRole(Role.Client, Role.Psychologist)]
        [HttpGet()]
        public ActionResult<List<GetAllPsychologistsResponse>> GetAllPsychologists()
        {
            return new List<GetAllPsychologistsResponse>();
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
        {
            return Created($"{this.GetRequestPath()}/{psychologistRequest.Id}", psychologistRequest.Id);
            //return psychologistRequest.Id;
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
        {
            return NoContent();
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpDelete("{id}")]
        public void DeletePsychologist(int id)
        {

        }

        [AuthorizeByRole(Role.Client, Role.Psychologist)]
        [HttpGet("{psychologistId}/comments")]
        public List<GetCommentsByPsychologistIdResponse> GetCommentsByPsychologistId(int psychologistId)
        {
            return new List<GetCommentsByPsychologistIdResponse>();
        }

        [AuthorizeByRole(Role.Client)]
        [HttpPost("request-psyhologist-search")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddRequestPsyhologistSearch([FromBody] RequestPsyhologistSearch requestPsyhologistSearch)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }

        [Authorize(Roles = nameof(Role.Client))]
        [HttpPost("{psychologistId}/comments")]
        public ActionResult <int> AddCommentForPsyhologist([FromBody] CommentRequest comment, int psychologistId)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }

    }
}
