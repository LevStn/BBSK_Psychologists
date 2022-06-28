using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
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

        [HttpGet]
        public List<OrderResponse> GetAllOrders()
        {
            return null;
        }

        [HttpGet("{orderId}")]
        public OrderResponse GetOrderById([FromRoute] int orderId)
        {
            return null;
        }

        [HttpPost]
        public void AddOrder([FromBody] OrderCreateRequest request)
        {

        }

        [HttpDelete("{orderId}")]
        public void DeleteOrderById([FromRoute] int orderId)
        {

        }

        [HttpPut("{orderId}")]
        public void UpdateOrderStatusByID([FromRoute] int orderId) 
        {

        }
    }
}
