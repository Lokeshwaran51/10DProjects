using MediatR;

namespace AmazonClone.API.Features.Product.Queries
{
    public class GetListOfProductsBySubCategoryIdQuery : IRequest<List<AmazonClone.API.Data.Entity.Product>>
    {
        public int SubCategoryId { get; set; }
        public GetListOfProductsBySubCategoryIdQuery(int subCategoryId)
        {
            SubCategoryId = subCategoryId;
        }
    }
}
