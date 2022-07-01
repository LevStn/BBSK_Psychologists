using BBSK_Psycho.Enums;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {

        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }


        [AllowAnonymous]
        [ProducesResponseType(typeof(int),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void),StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public ActionResult <int> AddClient([FromBody] ClientRegisterRequest client)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet("{id}")]
        public ActionResult< ClientResponse> GetClientById([FromRoute] int id)
        {
            return Ok (new ClientResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void),StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{id}")]
        public ActionResult UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {
            return NoContent();
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet("{id}/comments")]
        public ActionResult <CommentResponse> GetCommentsByClientId([FromRoute] int id)
        {

            return Ok(new List<CommentResponse>());
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet("{id}/orders")]
        public ActionResult <OrderResponse> GetOrdersByClientId([FromRoute] int id)
        {
            return Ok(new List<OrderResponse>());
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        public ActionResult DeleteClientById([FromRoute] int id)
        {
            return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet]
        public ActionResult<ClientResponse> GetClients()
        {
            return Ok(new List<ClientResponse>());
            
        }

    }
}
