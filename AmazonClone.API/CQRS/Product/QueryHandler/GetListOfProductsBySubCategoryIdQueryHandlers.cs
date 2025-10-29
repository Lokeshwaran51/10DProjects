using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Product.Queries;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products = AmazonClone.API.Data.Entity.Product;

namespace AmazonClone.API.CQRS.Product.QueryHandler
{
    public class GetListOfProductsBySubCategoryIdQueryHandlers : IRequestHandler<GetListOfProductsBySubCategoryIdQuery, List<Products>>
    {
        private readonly AppDbContext _context;
        public GetListOfProductsBySubCategoryIdQueryHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> Handle(GetListOfProductsBySubCategoryIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Products
                        .Where(p => p.SubCategoryId == query.SubCategoryId)
                        .ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
