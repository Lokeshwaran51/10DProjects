using AmazonClone.API.Data.Entity;
using CartServices.Cart.Commands;
using CartServices.Constants;
using CartServices.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CartServices.Cart.CommandHandlers
{
    public class RemoveItemFromCartCommandHandlers : IRequestHandler<RemoveItemFromCartCommand, string>
    {
        private readonly CartDbContext _context;
        public RemoveItemFromCartCommandHandlers(CartDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RemoveItemFromCartCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CartServices.Data.Entity.CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == command.ProductId, cancellationToken);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync(cancellationToken);
                    return ResponseMessages.removeItem;
                }

                return ResponseMessages.itemNotFound;
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
