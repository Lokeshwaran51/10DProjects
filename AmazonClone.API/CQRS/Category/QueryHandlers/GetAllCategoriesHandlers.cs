using AmazonClone.API.Data.Entity;
using AmazonClone.API.CQRS.Category.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AmazonClone.API.Data.DTO;

namespace AmazonClone.API.CQRS.Category.QueryHandlers
{
    public class GetAllCategoriesHandlers : IRequestHandler<GetAllCategoriesQuery, List<CategoryDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllCategoriesHandlers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken token)
        {
            List<CategoryDTO> categories = await _context.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                })
                .ToListAsync(token);

            return categories;
        }


    }
}
