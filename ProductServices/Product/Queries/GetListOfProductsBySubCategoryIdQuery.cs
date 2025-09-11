using MediatR;

namespace ProductServices.Product.Queries
{
    public class GetListOfProductsBySubCategoryIdQuery : IRequest<List<ProductServices.Data.Entity.Product>>
    {
        public int SubCategoryId { get; set; }
        public GetListOfProductsBySubCategoryIdQuery(int subCategoryId)
        {
            SubCategoryId = subCategoryId;
        }
    }
}
