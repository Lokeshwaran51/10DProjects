using AmazonClone.API.Features.Order.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
{
    //[Authorize]
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



        /* [HttpPost("Success")]
         public async Task<IActionResult> Success([FromBody] Order order)
         {
             try
             {
                 if (order == null)
                 {
                     return BadRequest("Invalid order data.");
                 }
                 order.OrderDate = DateTime.UtcNow;
                 await _context.Orders.AddAsync(order);
                 await _context.SaveChangesAsync();
                 return Ok(new
                 {
                     message = "Order received successfully.",
                     orderId = order.Id
                 });
             }
             catch (Exception ex)
             {
                 return StatusCode(500, "Internal server error: " + ex.Message);
             }
         }*/

        /* [HttpPost("PlaceOrder")]
         public async Task<IActionResult> PlaceOrder([FromBody] Order orderRequest)
         {
             try
             {
                 if (orderRequest == null)
                     return BadRequest("Invalid order data.");

                 // 1. Fetch product from database
                 var product = await _context.Products.FindAsync(orderRequest.Id);
                 if (product == null)
                     return NotFound("Product not found.");

                 // 2. Prepare order object
                 var order = new Order
                 {
                     Id = product.Id,
                     ProductName = product.Name,
                     Price = product.Price,
                     Quantity = orderRequest.Quantity,
                     Total = product.Price * orderRequest.Quantity,
                     OrderDate = DateTime.UtcNow,
                     UserId = orderRequest.UserId
                 };

                 // 3. Save order
                 _context.Orders.Add(order);
                 await _context.SaveChangesAsync();

                 return Ok(new { Message = "Order placed successfully", OrderId = order.OrderId });
             }
             catch (Exception ex)
             {
                 return StatusCode(500, "Internal server error: " + ex.Message);
             }
         }*/

    }
}
