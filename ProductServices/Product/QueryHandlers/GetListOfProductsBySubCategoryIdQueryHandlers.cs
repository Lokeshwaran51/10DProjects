using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductServices.Constants;
using ProductServices.Data.Context;
using ProductServices.Product.Queries;

namespace ProductServices.Product.QueryHandlers
{
    public class GetListOfProductsBySubCategoryIdQueryHandlers : IRequestHandler<GetListOfProductsBySubCategoryIdQuery, List<ProductServices.Data.Entity.Product>>
    {
        private readonly ProductDbContext _context;
        public GetListOfProductsBySubCategoryIdQueryHandlers(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductServices.Data.Entity.Product>> Handle(GetListOfProductsBySubCategoryIdQuery query, CancellationToken cancellationToken)
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
