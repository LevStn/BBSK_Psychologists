using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.Models.Responses;
namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [AuthorizeByRole]
        [ProducesResponseType(typeof(AllOrdersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<ActionResult<List<AllOrdersResponse>>> GetOrders()
        {
            ClaimModel claim = this.GetClaims();

            return Ok(_mapper.Map<List<AllOrdersResponse>>(await _ordersService.GetOrders(claim)));
        }


        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] int orderId)
        {
            ClaimModel claim = this.GetClaims();
            return Ok(_mapper.Map<OrderResponse>(await _ordersService.GetOrderById(orderId, claim)));
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPost]
        [ProducesResponseType(typeof(int),  StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> AddOrder([FromBody] OrderCreateRequest request)
        {
            ClaimModel claim = this.GetClaims();
            Order newOrder = _mapper.Map<Order>(request);
            newOrder.Client = new() {Id = claim.Id};
            newOrder.Psychologist = new() {Id = request.PsychologistId};
            await _ordersService.AddOrder(newOrder, claim);
            return Created($"{this.GetRequestPath()}/{newOrder.Id}", newOrder.Id);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteOrderById([FromRoute] int orderId)
        {
            ClaimModel claim = this.GetClaims();
            await _ordersService.DeleteOrder(orderId, claim);
            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpPatch("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> UpdateOrderStatusById([FromRoute] int orderId, [FromBody] OrderStatusPatchRequest orderStatusPatch)
        {
            ClaimModel claim = this.GetClaims();

            await _ordersService.UpdateOrderStatuses(orderId, orderStatusPatch.OrderStatus, orderStatusPatch.OrderPaymentStatus, claim);
            return NoContent();
        } 
    }
}