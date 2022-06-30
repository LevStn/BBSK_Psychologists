using BBSK_Psycho.Enums;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {

        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddClient([FromBody] ClientRegisterRequest client)
        {
            int id = 2;
            return Created($"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}/{id}", id);
        }

        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}")]
        public ClientResponse GetClientById([FromRoute] int id)
        {
            return new();
        }

        [AuthorizeByRole(Role.Client)]
        [HttpPut("{id}")]
        public ActionResult UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {
            return NoContent();
        }

        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/comments")]
        public List<CommentResponse> GetCommentsByClientId([FromRoute] int id)
        {
            return new();
        }

        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/orders")]
        public List<OrderResponse> GetOrdersByClientId([FromRoute] int id)
        {
            return null;
        }

        [AuthorizeByRole(Role.Client)]
        [HttpDelete("{id}")]
        public void DeleteClientById([FromRoute] int id)
        {

        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        public List<ClientResponse> GetClients()
        {
            return new();
        }

    }
}
