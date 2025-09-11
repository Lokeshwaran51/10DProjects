
using CartServices.Cart.Queries;
using CartServices.Constants;
using CartServices.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CartServices.Cart.QueryHandlers
{
    public class GetCartItemCountQueryHandler : IRequestHandler<GetCartItemCountQuery, int>
    {
        private readonly CartDbContext _context;

        public GetCartItemCountQueryHandler(CartDbContext context)
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
                CartServices.Data.Entity.Cart? cart = await _context.Carts
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
