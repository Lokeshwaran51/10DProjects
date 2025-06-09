using AmazonClone.API.Data.Entity;
using MediatR;

namespace AmazonClone.API.CQRS.Cart.Queries
{
    public class GetCartItemsQuery : IRequest<List<CartItemDto>>
    {
        public string Email { get; }

        public GetCartItemsQuery(string email)
        {
            Email = email;
        }
    }
}
