using CategoryServices.Data.DTOs;
using MediatR;

namespace CategoryServices.Category.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
