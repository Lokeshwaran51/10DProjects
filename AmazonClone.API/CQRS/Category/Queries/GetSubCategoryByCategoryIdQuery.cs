using MediatR;

namespace AmazonClone.API.CQRS.Category.Queries
{
    public record GetSubCategoryByCategoryIdQuery(int CategoryId) : IRequest<List<SubCategoryDto>>;

    public class SubCategoryDto
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
