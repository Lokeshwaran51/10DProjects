using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Category.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.Category.QueryHandlers
{
    public class GetAllCategoriesHandlers : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly AppDbContext _context;
        public GetAllCategoriesHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken token)
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                })
                .ToListAsync(token);

            return categories;
        }


    }
}
