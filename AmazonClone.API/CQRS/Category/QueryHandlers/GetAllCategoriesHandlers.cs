using AmazonClone.API.Constants;
using AmazonClone.API.CQRS.Category.Queries;
using AmazonClone.API.Data.DTO;
using AmazonClone.API.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.CQRS.Category.QueryHandlers
{
    public class GetAllCategoriesHandlers : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly AppDbContext _context;
        public GetAllCategoriesHandlers(AppDbContext context)
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
