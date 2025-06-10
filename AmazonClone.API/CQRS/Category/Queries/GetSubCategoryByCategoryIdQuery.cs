using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Category.Queries
{
    public class GetSubCategoryByCategoryIdQuery : IRequest<List<SubCategoryDto>> { 
        public int CategoryId { get; set; }
        public GetSubCategoryByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
    
}
