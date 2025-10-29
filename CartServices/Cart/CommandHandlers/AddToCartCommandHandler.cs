
using CartServices.Cart.Commands;
using CartServices.Constants;
using CartServices.Data.Context;
using CartServices.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CartServices.Cart.CommandHandlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, string>
    {
        private readonly CartDbContext _context;

        public AddToCartCommandHandler(CartDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Request;

                CartServices.Data.Entity.User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);
                if (user == null)
                {
                    return ResponseMessages.userNotFound;
                }

                CartServices.Data.Entity.Cart? cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.UserId, cancellationToken);
                if (cart == null)
                {
                    cart = new CartServices.Data.Entity.Cart
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

                CartServices.Data.Entity.CartItem? existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == dto.ProductId, cancellationToken);

                if (existingItem != null)
                {
                    existingItem.Quantity += dto.Quantity;
                }
                else
                {
                    CartItem cartItem = new CartServices.Data.Entity.CartItem
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
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
