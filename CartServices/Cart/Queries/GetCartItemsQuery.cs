using CartServices.Data.DTOs;
using MediatR;

namespace CartServices.Cart.Queries
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
