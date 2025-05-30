using AmazonClone.API.Data.Entity;
using MediatR;

namespace AmazonClone.API.Features.Cart.Queries
{
    public record GetCartItemsQuery(string Email) : IRequest<List<CartItemDto>>;
}
