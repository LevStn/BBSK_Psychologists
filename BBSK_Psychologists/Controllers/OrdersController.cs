using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace BBSK_Psycho.Controllers
{

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]

    public class OrdersController : ControllerBase
    {

        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet]
        public ActionResult<OrderResponse> GetAllOrders()
        {
            var orders = _ordersRepository.GetOrders();

            return Ok(orders);
        }


        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<OrderResponse> GetOrderById([FromRoute] int orderId)
        {
            Order order = _ordersRepository.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<int> AddOrder([FromBody] OrderCreateRequest request)
        {
            //Order newOrder = new Order()
            //{
            //    PsychologistId = request.PsychologistId,
            //    ClientId = request.ClientId,
            //    Cost = request.Cost,
            //    Duration = request.Duration,
            //    Message = request.Message,
            //    SessionDate = request.SessionDate,
            //    OrderDate = request.OrderDate,
            //    OrderStatus = OrderStatus.Created,
            //    OrderPaymentStatus = request.OrderPaymentStatus,
            //    IsDeleted = false
            //};

            Order newOrder = new Order();

            _mapper.Map(request, newOrder);

            _ordersRepository.AddOrder(newOrder);

            return Created($"{this.GetRequestPath()}/{newOrder.Id}", newOrder.Id);
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult DeleteOrder([FromRoute] int orderId)
        {
            _ordersRepository.DeleteOrder(orderId);

            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpPatch("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult UpdateOrderStatusById([FromRoute] int orderId, [FromBody] OrderStatusPatchRequest orderStatusPatch)
        {
            _ordersRepository.UpdateOrderStatus(orderId, (int)orderStatusPatch.OrderStatus, (int)orderStatusPatch.OrderPaymentStatus);

            return NoContent();
        }
    }
}
