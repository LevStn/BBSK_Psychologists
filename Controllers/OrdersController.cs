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
            return null;
        }

        [HttpGet("{orderId}")]
        public Client GetOrderById(int orderId)
        {
            return null;
        }

        [HttpPost()]
        public Order AddOrder()
        {
            return null;
        }

        [HttpDelete("{orderId}")]
        public Client DeleteOrderById(int orderId)
        {
            return null;
        }

        [HttpPut("{orderId}")]
        public Order UpdateOrderStatusByID(int orderId)
        {
            return null;
        }
    }
}
