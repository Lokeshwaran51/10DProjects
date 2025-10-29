using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Category.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
