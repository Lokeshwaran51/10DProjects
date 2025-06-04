using MediatR;

namespace AmazonClone.API.Features.Cart.Queries
{
    public class GetCartItemCountQuery : IRequest<int>
    {
        public string Email { get; }
        public GetCartItemCountQuery(string email)
        {
            Email = email;
        }
    }
}
