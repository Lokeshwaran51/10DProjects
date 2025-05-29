using AmazonClone.API.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace AmazonClone.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [Route("Cart")]
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

        [HttpGet("GetCartItems/{email}")]
        public async Task<IActionResult> GetCartItems(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email is required.");
                }

                var cart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.Email == email);

                if (cart == null)
                {
                    return NotFound("Cart not found for this user.");
                }

                var cartItems = await _context.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .Join(_context.Products,
                        cartItem => cartItem.ProductId,
                        product => product.Id,
                        (cartItem, product) => new CartItemDto
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            Quantity = cartItem.Quantity ?? 0,
                            Price = product.Price,
                            ImageUrl = product.ImageUrl,
                            Description = product.Description,
                            Total = (cartItem.Quantity ?? 0) * product.Price
                        })
                    .ToListAsync();
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromForm] string Email, [FromForm] int productId, [FromForm] int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return BadRequest("User email is required.");
                }

                // Ensure user exists
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Ensure cart exists for the user
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.UserId);
                if (cart == null)
                {
                    // Create a new cart for the user
                    cart = new Cart
                    {
                        UserId = user.UserId,
                        Email = Email
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync(); // Save to generate CartId
                }

                // Ensure product exists
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                // Check if product is already in cart
                var existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

                if (existingItem != null)
                {
                    // Update quantity
                    existingItem.Quantity += quantity;
                }
                else
                {
                    // Add new item to cart
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = quantity,
                        Price = product.Price,
                        UserId = user.UserId
                    };
                    await _context.CartItems.AddAsync(cartItem);
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Product added to cart." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("CartItemCount")]
        public async Task<IActionResult> CartItemCount([FromQuery] string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return BadRequest("Email is required.");
                }

                var cart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.Email == Email);

                if (cart == null)
                {
                    return NotFound("Cart not found for this user.");
                }

                int count = await _context.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .SumAsync(ci => ci.Quantity ?? 0);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromBody] int ProductId)
        {
            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == ProductId);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Item removed successfully." });
                }

                return NotFound("Cart item not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
