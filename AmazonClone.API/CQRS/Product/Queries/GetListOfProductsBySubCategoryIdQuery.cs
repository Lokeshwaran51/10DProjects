using MediatR;

namespace AmazonClone.API.CQRS.Product.Queries
{
    public class GetListOfProductsBySubCategoryIdQuery : IRequest<List<Data.Entity.Product>>
    {
        public int SubCategoryId { get; set; }
        public GetListOfProductsBySubCategoryIdQuery(int subCategoryId)
        {
            SubCategoryId = subCategoryId;
        }
    }
}
