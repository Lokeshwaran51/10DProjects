using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Product.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.Product.QueryHandler
{
    public class GetListOfProductsBySubCategoryIdQueryHandlers : IRequestHandler<GetListOfProductsBySubCategoryIdQuery, List<AmazonClone.API.Data.Entity.Product>>
    {
        private readonly AppDbContext _context;
        public GetListOfProductsBySubCategoryIdQueryHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AmazonClone.API.Data.Entity.Product>> Handle(GetListOfProductsBySubCategoryIdQuery query, CancellationToken token)
        {
            return await _context.Products
                 .Where(p => p.SubCategoryId == query.SubCategoryId)
                 .ToListAsync();
        }
    }
}
