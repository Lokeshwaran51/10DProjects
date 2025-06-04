using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Order.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.Order.CommandHelper
{
    public class SuccessCommandHelpers : IRequestHandler<SuccessCommand, List<AmazonClone.API.Data.Entity.Order>>
    {
        private readonly AppDbContext _context;

        public SuccessCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AmazonClone.API.Data.Entity.Order>> Handle(SuccessCommand command, CancellationToken token)
        {
            try
            {
                if (command.Order == null)
                {
                    throw new ArgumentNullException(nameof(command.Order), "Invalid order data.");
                }

                command.Order.OrderDate = DateTime.UtcNow;
                await _context.Orders.AddAsync(command.Order, token);
                await _context.SaveChangesAsync(token);

                var orders = await _context.Orders.ToListAsync(token);
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Internal server error: " + ex.Message);
            }
        }
    }
}