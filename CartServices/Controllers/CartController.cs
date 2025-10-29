
using CartServices.Cart.Commands;
using CartServices.Cart.Queries;
using CartServices.Constants;
using CartServices.Data.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CartServices.Controllers
{
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
        public async Task<IActionResult> AddToCart(AddToCartRequestDto addToCartdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string result = await _mediator.Send(new AddToCartCommand(addToCartdto));
                return Ok(result);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
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
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
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
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }

        [HttpPost("RemoveItemFromCart")]
        public async Task<IActionResult> RemoveItemFromCart([FromQuery][Required] int ProductId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string res = await _mediator.Send(new RemoveItemFromCartCommand(ProductId));
                return Ok(res);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
