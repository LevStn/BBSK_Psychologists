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
        public int AddClient([FromBody] ClientRegisterRequest request)
        {
            return 2;
        }


        [HttpGet("{id}")]
        public ClientResponse GetClientById(int id)
        {
            return new();
        }

        [HttpPut("{id}")]
        public void UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {

        }
       
        [HttpGet("{id}/comments")]
        public List<CommentResponse> GetCommentsByClientId(int id)
        {
            return new();
        }

        [HttpGet("{id}/orders")]
        public List<OrderResponse> GetOrdersByClientId(int id)
        {
            return null;
        }

        [AllowAnonymous]
        [HttpPost("request-search")]
        public string AddRequestSearch([FromBody] RequestSearch requestSearch)
        {
            return "DONE";
        }

        [AuthorizeByRole(Role.Client)]
        [HttpDelete("{id}")]
        public void DeleteClientById([FromRoute] int id)
        {

        }

        [AuthorizeByRole]
        [HttpGet]
        public List<ClientResponse> GetClients()
        {
            return new();
        }

    }
}
