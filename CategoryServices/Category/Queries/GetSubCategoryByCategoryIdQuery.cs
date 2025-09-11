using CategoryServices.Data.DTOs;
using MediatR;

namespace CategoryServices.Category.Queries
{
    public class GetSubCategoryByCategoryIdQuery : IRequest<List<SubCategoryDto>>
    {
        public int CategoryId { get; set; }
        public GetSubCategoryByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
