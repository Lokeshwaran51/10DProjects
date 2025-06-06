using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Product.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products = AmazonClone.API.Data.Entity.Product;

namespace AmazonClone.API.Features.Product.QueryHandler
{
    public class GetListOfProductsBySubCategoryIdQueryHandlers : IRequestHandler<GetListOfProductsBySubCategoryIdQuery, List<Products>>
    {
        private readonly AppDbContext _context;
        public GetListOfProductsBySubCategoryIdQueryHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> Handle(GetListOfProductsBySubCategoryIdQuery query, CancellationToken token)
        {
            return await _context.Products
                 .Where(p => p.SubCategoryId == query.SubCategoryId)
                 .ToListAsync();
        }
    }
}
