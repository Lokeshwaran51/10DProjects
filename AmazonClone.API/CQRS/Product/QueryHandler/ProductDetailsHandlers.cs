using AmazonClone.API.CQRS.Product.Queries;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.CQRS.Product.QueryHandler
{
    public class ProductDetailsHandlers : IRequestHandler<ProductDetailsQuery, ProductDetailsQuery.ProductDetailsDto>
    {
        private readonly AppDbContext _context;
        public ProductDetailsHandlers(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductDetailsQuery.ProductDetailsDto> Handle(ProductDetailsQuery query, CancellationToken token)
        {
            try
            {
                var product = await _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.SubCategory)
                        .FirstOrDefaultAsync(p => p.Id == query.ProductId, token);

                if (product == null)
                {
                    throw new Exception($"Product with ID {query.ProductId} not found.");
                }

                return new ProductDetailsQuery.ProductDetailsDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CategoryName = product.Category?.Name,
                    SubCategoryName = product.SubCategory?.SubCategoryName ?? product.SubCategoryName
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
