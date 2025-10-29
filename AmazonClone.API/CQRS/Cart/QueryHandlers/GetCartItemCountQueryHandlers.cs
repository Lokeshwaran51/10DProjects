using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Cart.Queries;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Carts = AmazonClone.API.Data.Entity.Cart;

namespace AmazonClone.API.CQRS.Cart.QueryHandlers
{
    public class GetCartItemCountQueryHandler : IRequestHandler<GetCartItemCountQuery, int>
    {
        private readonly AppDbContext _context;

        public GetCartItemCountQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetCartItemCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return 0;
                }
                Carts? cart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);

                if (cart == null)
                {
                    return 0;
                }
                int count = await _context.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .SumAsync(ci => ci.Quantity ?? 0, cancellationToken);
                return count;
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
