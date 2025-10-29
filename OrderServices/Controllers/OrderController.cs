using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data.DTOs;
using OrderServices.Order.Command;

namespace OrderServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Success")]
        public async Task<IActionResult> Success(SuccessCommand command)
        {
            try
            {
                List<OrderDto> res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Internal Server Error.");
            }
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            try
            {
                List<OrderDto> res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Internal Server Error.");
            }
        }
    }
}
