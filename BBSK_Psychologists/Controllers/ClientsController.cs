using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
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
        private readonly IClientsRepository _clientsRepository;
      
        public ClientsController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddClient([FromBody] ClientRegisterRequest client)
        {
            var clientModel = new Client
            {
                Name = client.Name,
                LastName = client.LastName,
                Password = client.Password,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                BirthDate = client.BirthDate

            };
            
            var result = _clientsRepository.AddClient(clientModel);
            return Created("", result);
            
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult< ClientResponse> GetClientById([FromRoute] int id)
        {
            var clinet = _clientsRepository.GetClientById(id);

            if (clinet is null)
                return NotFound();
            else
                return Ok(clinet);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {
            var client = new Client()
            {
                Name = request.Name,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };
            

            _clientsRepository.UpdateClient(client, id);

            return NoContent();
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult <CommentResponse> GetCommentsByClientId([FromRoute] int id)
        {
            var clientComents = _clientsRepository.GetCommentsByClientId(id);
            if (clientComents is null)
                return NotFound();
            else
                return Ok(clientComents);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/orders")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult <OrderResponse> GetOrdersByClientId([FromRoute] int id)
        {
            var clientOrders = _clientsRepository.GetOrdersByClientId(id);
            if(clientOrders is null)
                return NotFound();
            else
                return Ok(clientOrders);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult DeleteClientById([FromRoute] int id)
        {
            var client = _clientsRepository.GetClientById(id);
            if (client is null)
                return NotFound();
            else
                _clientsRepository.DeleteClient(id);
                return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<ClientResponse> GetClients()
        {
            var clients = _clientsRepository.GetClients();
            return Ok(clients);


        }

    }
}
