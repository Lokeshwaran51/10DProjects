using AmazonClone.API.CQRS.Order.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
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
        public async Task<IActionResult> Success([FromBody] SuccessCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
