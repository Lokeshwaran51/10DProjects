using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.Category.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.Category.QueryHandlers
{
    public class GetSubCategoryByCategoryIdQueryHandler : IRequestHandler<GetSubCategoryByCategoryIdQuery, List<SubCategoryDto>>
    {
        private readonly AppDbContext _context;

        public GetSubCategoryByCategoryIdQueryHandler(AppDbContext context)
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
            catch (Exception ex)
            {
                return new List<SubCategoryDto>();
            }
        }
    }
}
