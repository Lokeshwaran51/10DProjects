using CategoryServices.Category.Queries;
using CategoryServices.Constants;
using CategoryServices.Data.Context;
using CategoryServices.Data.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CategoryServices.Category.QueryHandlers
{
    public class GetSubCategoryByCategoryIdQueryHandler : IRequestHandler<GetSubCategoryByCategoryIdQuery, List<SubCategoryDto>>
    {
        private readonly CategoryDbContext _context;

        public GetSubCategoryByCategoryIdQueryHandler(CategoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubCategoryDto>> Handle(GetSubCategoryByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<SubCategoryDto> subcategories = await _context.SubCategories
                    .Include(sc => sc.Category)
                    .Where(sc => sc.CategoryId == request.CategoryId)
                    .Select(sc => new SubCategoryDto
                    {
                        SubCategoryId = sc.SubCategoryId,
                        SubCategoryName = sc.SubCategoryName,
                        CategoryId = (int)sc.CategoryId,
                        CategoryName = sc.Category.Name
                    })
                    .ToListAsync(cancellationToken);

                return subcategories;
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
