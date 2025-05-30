using AmazonClone.API.Data.Entity;
using MediatR;

namespace AmazonClone.API.Features.Cart.Queries
{
    public record GetCartItemCountQuery(string Email) : IRequest<List<CartItemDto>>;
}
