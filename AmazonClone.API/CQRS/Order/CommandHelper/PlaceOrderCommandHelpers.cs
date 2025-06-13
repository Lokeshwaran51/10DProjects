using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Order.Command;
using AmazonClone.API.Data;
using AmazonClone.API.Data.DTO;
using AmazonClone.API.Data.Entity;
using Product = AmazonClone.API.Data.Entity.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders = AmazonClone.API.Data.Entity.Order;

namespace AmazonClone.API.CQRS.Order.CommandHelper
{
    public class PlaceOrderCommandHelpers : IRequestHandler<PlaceOrderCommand, List<OrderDto>>
    {
        private readonly AppDbContext _context;

        public PlaceOrderCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AmazonClone.API.Data.Entity.Product? product = await _context.Products.FindAsync(new object[] { command.ProductId }, cancellationToken);
                if (product == null)
                {
                    throw new InvalidOperationException(ResponseMessages.productNotFound);
                }

                AmazonClone.API.Data.Entity.Order newOrder = new Orders
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