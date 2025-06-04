using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Cart.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.Cart.CommandHandlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, string>
    {
        private readonly AppDbContext _context;

        public AddToCartCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AddToCartCommand request, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return "User not found.";
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.UserId);
            if (cart == null)
            {
                cart = new Data.Entity.Cart
                {
                    UserId = user.UserId,
                    Email = request.Email
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(token);
            }

            var product = await _context.Products.FindAsync(new object[] { request.ProductId }, token);
            if (product == null)
            {
                return "Product not found.";
            }

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == request.ProductId, token);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = request.Quantity,
                    Price = product.Price,
                    UserId = user.UserId
                };
                await _context.CartItems.AddAsync(cartItem, token);
            }

            await _context.SaveChangesAsync(token);
            return "Product added to cart.";
        }
    }
}