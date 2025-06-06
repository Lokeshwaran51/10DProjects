using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Order.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products = AmazonClone.API.Data.Entity.Product;
using Orders = AmazonClone.API.Data.Entity.Order;

namespace AmazonClone.API.Features.Order.CommandHelper
{
    public class PlaceOrderCommandHelpers : IRequestHandler<PlaceOrderCommand, List<AmazonClone.API.Data.Entity.Order>>
    {
        private readonly AppDbContext _context;
        public PlaceOrderCommandHelpers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AmazonClone.API.Data.Entity.Order>> Handle(PlaceOrderCommand command, CancellationToken token)
        {
            Products product = await _context.Products.FindAsync(new object[] { command.ProductId }, token);
            if (product == null)
            {
                throw new Exception(ResponseMessages.productNotFound);
            }

            Orders order = new AmazonClone.API.Data.Entity.Order
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
    }
}
