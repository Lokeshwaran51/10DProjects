using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users = AmazonClone.API.Data.Entity.User;
using Carts = AmazonClone.API.Data.Entity.Cart;
using CartItems = AmazonClone.API.Data.Entity.CartItem;
using Products = AmazonClone.API.Data.Entity.Product;
using AmazonClone.API.CQRS.Cart.Commands;

namespace AmazonClone.API.CQRS.Cart.CommandHandlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, string>
    {
        private readonly AppDbContext _context;

        public AddToCartCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Request;
                Users? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);
                if (user == null)
                {
                    return ResponseMessages.userNotFound;
                }

                Carts? cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.UserId, cancellationToken);
                if (cart == null)
                {
                    cart = new Carts
                    {
                        UserId = user.UserId,
                        Email = dto.Email
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var product = await _context.Products.FindAsync(new object[] { dto.ProductId }, cancellationToken);
                if (product is null)
                {
                    throw new InvalidOperationException(ResponseMessages.productNotFound);
                }

                CartItems? existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == dto.ProductId, cancellationToken);

                if (existingItem != null)
                {
                    existingItem.Quantity += dto.Quantity;
                }
                else
                {
                    CartItems cartItem = new CartItems
                    {
                        CartId = cart.CartId,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = dto.Quantity,
                        Price = product.Price,
                        UserId = user.UserId
                    };
                    await _context.CartItems.AddAsync(cartItem, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return ResponseMessages.addedToCart;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while adding item to cart.", ex);
            }
        }
    }
}