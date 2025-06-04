using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Cart.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.Cart.CommandHandlers
{
    public class RemoveItemFromCartCommandHandlers : IRequestHandler<RemoveItemFromCartCommand, string>
    {
        private readonly AppDbContext _context;
        public RemoveItemFromCartCommandHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RemoveItemFromCartCommand command, CancellationToken token)
        {
            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == command.ProductId);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return "Item removed successfully.";
                }

                return "Cart item not found.";
            }
            catch (Exception ex)
            {
                return $"Internal server error: {ex.Message}";
            }
        }
    }
}
