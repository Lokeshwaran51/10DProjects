using AmazonClone.API.CQRS.Cart.Commands;
using AmazonClone.API.CQRS.Cart.Queries;
using AmazonClone.API.Data.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(AddToCartRequestDTO addToCartdto)
        {
            try
            {
                string result = await _mediator.Send(new AddToCartCommand(addToCartdto));
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetCartItems/{email}")]
        public async Task<IActionResult> GetCartItems(string email)
        {
            try
            {
                List<CartItemDto> result = await _mediator.Send(new GetCartItemsQuery(email));
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("CartItemCount")]
        public async Task<IActionResult> CartItemCount(string Email)
        {
            try
            {
                int result = await _mediator.Send(new GetCartItemCountQuery(Email));
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("RemoveItemFromCart")]
        public async Task<IActionResult> RemoveItemFromCart(int ProductId)
        {
            try
            {
                string res = await _mediator.Send(new RemoveItemFromCartCommand(ProductId));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500,  ex.Message);
            }
        }
    }
}