using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Cart.Queries;
using AmazonClone.API.Data.DTO;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Carts = AmazonClone.API.Data.Entity;

namespace AmazonClone.API.CQRS.Cart.QueryHandlers
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, List<CartItemDto>>
    {
        private readonly AppDbContext _context;

        public GetCartItemsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AmazonClone.API.Data.Entity.Cart? cart = await _context.Carts.FirstOrDefaultAsync(c => c.Email == request.Email,cancellationToken);
                if (cart == null)
                {
                    return [];
                }

                return await _context.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .Join(_context.Products,
                          ci => ci.ProductId,
                          p => p.Id,
                          (ci, p) => new CartItemDto
                          {
                              ProductId = p.Id,
                              ProductName = p.Name,
                              Quantity = ci.Quantity ?? 0,
                              Price = p.Price,
                              ImageUrl = p.ImageUrl,
                              Description = p.Description,
                              Total = (ci.Quantity ?? 0) * p.Price
                          }).ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}