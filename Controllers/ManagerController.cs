using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;


        public ManagerController(ILogger<ManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{orders}")]
        public Client GetAllOrders()
        {
            return null!;
        }

        [HttpGet("{orders}/{id}")]
        public Client GetOrderById(int id)
        {
            return null!;
        }

        [HttpGet("{orders}/{sessions}")]
        public Client GetSessionById(int id)
        {
            return null!;
        }

        [HttpGet("{orders}/{sessions}/{id}")]
        public Client GetAllSessions(int id)
        {
            return null!;
        }
    }
}
