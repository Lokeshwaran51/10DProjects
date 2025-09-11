using MediatR;

namespace CartServices.Cart.Queries
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
