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

        [HttpGet("{id}/{comments}")]
        public List<Comment> GetCommentsById(int id)
        {
            return null;
        }


        [HttpPut("{id}/{comments}/{comments-id}")]
        public List<Comment> UpdateCommentsById(int id)
        {
            return null;
        }


     
        [HttpDelete("{id}/{comments}/{comment-id}")]
        public List<Comment> DeleteClientCommentById(int id)
        {
            return null;
        }

    }
}
