using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Order.Command;
using AmazonClone.API.Data;
using AmazonClone.API.Data.DTO;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders = AmazonClone.API.Data.Entity.Order;

namespace AmazonClone.API.CQRS.Order.CommandHelper
{
    public class PlaceOrderCommandHelpers : IRequestHandler<PlaceOrderCommand, List<OrderDTO>>
    {
        private readonly AppDbContext _context;

        public PlaceOrderCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDTO>> Handle(PlaceOrderCommand command, CancellationToken token)
        {
            try
            {
                var product = await _context.Products.FindAsync(new object[] { command.ProductId }, token);
                if (product == null)
                {
                    throw new Exception(ResponseMessages.productNotFound);
                }

                var newOrder = new Orders
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = command.Quantity,
                    Total = product.Price * command.Quantity,
                    OrderDate = DateTime.UtcNow,
                    UserId = command.UserId
                };

                await _context.Orders.AddAsync(newOrder, token);
                await _context.SaveChangesAsync(token);

                // Convert Entity List to DTO List
                var orderList = await _context.Orders.ToListAsync(token);
                var orderDTOList = orderList.Select(o => new OrderDTO
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
