using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrdersController
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public Client GetAllOrders()
        {
            return null!;
        }

        [HttpGet("{id}")]
        public Client GetOrderById(int id)
        {
            return null!;
        }

        [HttpPost()]
        public Order AddOrder()
        {
            return null!;
        }

        [HttpDelete("{id}")]
        public Client DeleteOrderById(int id)
        {
            return null!;
        }

        [HttpGet("{sessions}")]
        public Client GetSessionById(int id)
        {
            return null!;
        }

        [HttpGet("{sessions}/{id}")]
        public Client GetAllSessions(int id)
        {
            return null!;
        }


    }
}
