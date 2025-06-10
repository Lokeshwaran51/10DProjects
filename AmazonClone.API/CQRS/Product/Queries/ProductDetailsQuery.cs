using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Product.Queries
{
    public class ProductDetailsQuery : IRequest<ProductDetailsQuery.ProductDetailsDto>
    {
        public int ProductId { get; set; }
        public ProductDetailsQuery(int productId)
        {
            ProductId = productId;
        }
        public class ProductDetailsDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public string CategoryName { get; set; }
            public string SubCategoryName { get; set; }
        }
    }
}
