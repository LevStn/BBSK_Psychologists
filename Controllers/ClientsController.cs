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
            return new Client();
        }

        [HttpGet()]
        public List<Client> GetClients()
        {
            return new List<Client>();
        }

        [HttpPost()]
        public Client AddClient(int id)
        {
            return new Client();
        }

        [HttpPut("{id}")]
        public Client UpdateClientById(int id)
        {
            return new Client();
        }

        [HttpGet("{comments}/{id}")]
        public List<Comment> GetCommentsById()
        {
            return new List<Comment>();
        }
    }
}
