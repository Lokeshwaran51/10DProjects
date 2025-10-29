using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderServices.Constants;
using OrderServices.Data.Context;
using OrderServices.Data.DTOs;
using OrderServices.Order.Command;

namespace OrderServices.Order.CommandHandlers
{
    public class PlaceOrderCommandHandlers : IRequestHandler<PlaceOrderCommand, List<OrderDto>>
    {
        private readonly OrderDbContext _context;

        public PlaceOrderCommandHandlers(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                OrderServices.Data.Entity.Product? product = await _context.Products.FindAsync(new object[] { command.ProductId }, cancellationToken);
                if (product == null)
                {
                    throw new InvalidOperationException(ResponseMessages.productNotFound);
                }

                OrderServices.Data.Entity.Order newOrder = new OrderServices.Data.Entity.Order
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = command.Quantity,
                    Total = product.Price * command.Quantity,
                    OrderDate = DateTime.UtcNow,
                    UserId = command.UserId
                };

                await _context.Orders.AddAsync(newOrder, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                // Convert Entity List to DTO List
                var orderList = await _context.Orders.ToListAsync(cancellationToken);
                var orderDTOList = orderList.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    ProductName = o.ProductName,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    Total = o.Total,
                    OrderDate = o.OrderDate,
                    PaymentMode = o.PaymentMode,
                    UserId = o.UserId
                }).ToList();

                return orderDTOList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while placing the order.", ex);
            }
        }
    }
}
