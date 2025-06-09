using MediatR;

namespace AmazonClone.API.CQRS.Cart.Commands
{
    public class AddToCartCommand : IRequest<string>
    {
        public string Email { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
