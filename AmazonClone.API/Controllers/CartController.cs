using AmazonClone.API.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace AmazonClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CartController(AppDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        /* [HttpPost("AddToCart")]
         public async Task<IActionResult> AddToCart([FromBody] CartItem cartItemDto)
         {
             if (cartItemDto == null)
                 return BadRequest("Invalid data");

             var cartItem = new CartItem
             {
                 CartId = cartItemDto.CartId,
                 ProductName = cartItemDto.ProductName,
                 Quantity = cartItemDto.Quantity,
                 Price = cartItemDto.Price
             };

             _context.CartItems.Add(cartItem);
             await _context.SaveChangesAsync();

             return Ok(new { message = "Item added to cart" });
         }*/


        [HttpGet("AddToCart")]
        public async Task<IActionResult> AddToCart(int userId,int ProductId, int quantity)
        {
            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                var product = await _context.Products.FindAsync(ProductId);
                if (product == null)
                    return NotFound("Product not found.");

                var existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductName == product.Name);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductName = product.Name,
                        Quantity = quantity,
                        Price = product.Price
                    };
                    _context.CartItems.Add(cartItem);
                }
                await _context.SaveChangesAsync();
                return Ok(new { message = "Product added to cart." });
            }   
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
