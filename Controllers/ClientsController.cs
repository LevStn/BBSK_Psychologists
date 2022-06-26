using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ClientsController : ControllerBase
    {

        private readonly ILogger<ClientsController> _logger;


        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }


        [HttpGet("{id}")]
        public Client GetClientById(int id)
        {
            return null;
        }


        [HttpPost()]
        public Client AddClient()
        {
            return null;
        }


        [HttpGet()]
        public List<Client> GetClients()
        {
            return null;
        }


        [HttpPut("{id}")]
        public Client UpdateClientById(int id)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public Client DeleteClientById(int id)
        {
            return null;
        }

        [HttpGet("{id}/comments")]
        public List<Comment> GetCommentsByClientId(int id)
        {
            return null;
        }


        [HttpGet("{clientId}/comments/{commentId}")]
        public Comment GetCommentById(int clientId, int commentId)
        {
            return null;
        }


        [HttpPut("{clientId}/comments/{commentId}")]
        public List<Comment> UpdateCommentById(int clientId, int commentId)
        {
            return null;
        }

        [HttpPost("{id}/comments")]
        public List <Comment> AddComment(int id)
        {
            return null;
        }

        [HttpDelete("{clientId}/comments/{commentId}")]
        public List < Comment> DeleteClientCommentById(int clientId, int commentId)
        {
            return null;
        }
    }
}
