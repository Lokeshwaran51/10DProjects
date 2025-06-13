using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Cart.Commands;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.CQRS.Cart.CommandHandlers
{
    public class RemoveItemFromCartCommandHandlers : IRequestHandler<RemoveItemFromCartCommand, string>
    {
        private readonly AppDbContext _context;
        public RemoveItemFromCartCommandHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RemoveItemFromCartCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == command.ProductId,cancellationToken);
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
