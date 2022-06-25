using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;


        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Client GetUserById(int id)
        {
            return new Client();
        }

        [HttpGet()]
        public List<Client> GetUsers()
        {
            return new List<Client>();
        }

        [HttpPost()]
        public Client AddUser(int id)
        {
            return new Client();
        }

        [HttpPut("{id}")]
        public Client UpdateUserById(int id)
        {
            return new Client();
        }

        [HttpGet("{id}")]
        public List<Comment> GetCommentsById()
        {
            return new List<Comment>();
        }
    }
}
