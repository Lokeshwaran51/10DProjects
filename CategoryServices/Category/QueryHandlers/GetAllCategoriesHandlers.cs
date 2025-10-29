using CategoryServices.Category.Queries;
using CategoryServices.Constants;
using CategoryServices.Data.Context;
using CategoryServices.Data.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CategoryServices.Category.QueryHandlers
{
    public class GetAllCategoriesHandlers : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly CategoryDbContext _context;
        public GetAllCategoriesHandlers(CategoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<CategoryDto> categories = await _context.Categories
                       .Select(c => new CategoryDto
                       {
                           CategoryId = c.CategoryId,
                           Name = c.Name
                       })
                       .ToListAsync(cancellationToken);
                return categories;
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
