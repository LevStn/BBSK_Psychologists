using BBSK_Psycho.Enums;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{

    [ApiController]
    [Authorize]
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

        [Authorize(Roles = nameof(Role.Client))]
        [HttpPost]
        public void AddOrder([FromBody] OrderCreateRequest request)
        {

        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpDelete("{orderId}")]
        public void DeleteOrderById([FromRoute] int orderId)
        {

        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpPut("{orderId}")]
        public void UpdateOrderStatusByID([FromRoute] int orderId) 
        {

        }
    }
}
