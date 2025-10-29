using AmazonClone.API.Data.DTO;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.CQRS.Order.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.CQRS.Order.CommandHelper
{
    public class SuccessCommandHelpers : IRequestHandler<SuccessCommand, List<OrderDto>>
    {
        private readonly AppDbContext _context;

        public SuccessCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> Handle(SuccessCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Order == null)
                    throw new InvalidOperationException("Invalid order data.");

                // Manual mapping from DTO to Entity
                var orderEntity = new AmazonClone.API.Data.Entity.Order
                {
                    Id = command.Order.ProductId ?? 0,
                    ProductName = command.Order.ProductName,
                    Price = command.Order.Price ?? 0,
                    Quantity = command.Order.Quantity ?? 0,
                    Total = command.Order.Total ?? 0,
                    OrderDate = DateTime.UtcNow,
                    PaymentMode = command.Order.PaymentMode,
                    UserId = command.Order.UserId
                };

                await _context.Orders.AddAsync(orderEntity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                // Return updated list as DTOs
                var orders = await _context.Orders
                    .Select(o => new OrderDto
                    {
                        OrderId = o.OrderId,
                        ProductId = o.Id,
                        ProductName = o.ProductName,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        Total = o.Total,
                        OrderDate = o.OrderDate,
                        PaymentMode = o.PaymentMode,
                        UserId = o.UserId
                    })
                    .ToListAsync(cancellationToken);

                return orders;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An internal error occurred while processing the order.", ex);
            }
        }
    }
}
