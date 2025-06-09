using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.CQRS.Order.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products = AmazonClone.API.Data.Entity.Product;
using Orders = AmazonClone.API.Data.Entity.Order;

namespace AmazonClone.API.CQRS.Order.CommandHelper
{
    public class PlaceOrderCommandHelpers : IRequestHandler<PlaceOrderCommand, List<Orders>>
    {
        private readonly AppDbContext _context;
        public PlaceOrderCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orders>> Handle(PlaceOrderCommand command, CancellationToken token)
        {
            try
            {
                Products product = await _context.Products.FindAsync(new object[] { command.ProductId }, token);
                if (product == null)
                {
                    throw new Exception(ResponseMessages.productNotFound);
                }

                Orders order = new Orders
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = command.Quantity,
                    Total = product.Price * command.Quantity,
                    OrderDate = DateTime.UtcNow,
                    UserId = command.UserId
                };
                await _context.Orders.AddAsync(order, token);
                await _context.SaveChangesAsync(token);
                return await _context.Orders.ToListAsync(token);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
