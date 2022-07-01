using BBSK_Psycho.Enums;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]

    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet]
        public ActionResult<OrderResponse> GetAllOrders()
        {
            return Ok(new List <OrderResponse>());

        }


        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpGet("{orderId}")]
        public ActionResult<OrderResponse> GetOrderById([FromRoute] int orderId)
        {
            return Ok(new OrderResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPost]
        public ActionResult<int> AddOrder([FromBody] OrderCreateRequest request)
        {
            int id = 2;
            return Created($"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}/{id}", id);
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpDelete("{orderId}")]
        public ActionResult DeleteOrderById([FromRoute] int orderId)
        {
            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [HttpPatch("{orderId}")]
        public ActionResult UpdateOrderStatusById([FromRoute] int orderId, [FromBody] OrderStatusPatchRequest orderStatusPatch)
        {
            return NoContent();
        }

        
    }
}
