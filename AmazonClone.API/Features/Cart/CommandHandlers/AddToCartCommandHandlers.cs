using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Cart.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users = AmazonClone.API.Data.Entity.User;
using Carts = AmazonClone.API.Data.Entity.Cart;
using CartItems = AmazonClone.API.Data.Entity.CartItem;
using Products = AmazonClone.API.Data.Entity.Product;

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
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return ResponseMessages.userNotFound;
            }

            Carts cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.UserId);
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

            Products product = await _context.Products.FindAsync(new object[] { request.ProductId }, token);
            if (product == null)
            {
                return ResponseMessages.productNotFound;
            }

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == request.ProductId, token);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                CartItems cartItem = new CartItem
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
            return ResponseMessages.addedToCart;
        }
    }
}