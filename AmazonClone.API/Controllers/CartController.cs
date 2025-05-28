using AmazonClone.API.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace AmazonClone.API.Controllers
{
    [Authorize]
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

        [HttpGet("GetCartItems/{email}")]
        public async Task<IActionResult> GetCartItems(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email is required.");
                }

                int userId = await _context.Users
           .Where(u => u.Email == email)
           .Select(u => u.UserId)
           .FirstOrDefaultAsync();

                var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId)
            .Join(_context.Products,
                  cart => cart.ProductId,
                  product => product.Id,
                  (cart, product) => new CartItemDto
                  {
                      ProductId = product.Id,
                      ProductName = product.Name,
                      Quantity = cart.Quantity ?? 0,
                      Price = product.Price,
                      Total = (cart.Quantity ?? 0) * product.Price
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
                    // Optionally create a new cart if not found
                    cart = new Cart
                    {
                        UserId = user.UserId,
                        Email = Email
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync(); // Save to get the new CartId
                }

                // Ensure product exists
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                // Check if the product is already in the cart
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


    }
}
