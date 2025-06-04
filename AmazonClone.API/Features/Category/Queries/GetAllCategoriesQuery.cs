using MediatR;

namespace AmazonClone.API.Features.Category.Queries
{
    public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;
}
public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
}
