using AmazonClone.API.Data.Entity;
using AmazonClone.API.CQRS.Order.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders = AmazonClone.API.Data.Entity.Order;

namespace AmazonClone.API.CQRS.Order.CommandHelper
{
    public class SuccessCommandHelpers : IRequestHandler<SuccessCommand, List<Orders>>
    {
        private readonly AppDbContext _context;

        public SuccessCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orders>> Handle(SuccessCommand command, CancellationToken token)
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

                List<Orders> orders = await _context.Orders.ToListAsync(token);
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("Internal server error: " + ex.Message);
            }
        }
    }
}