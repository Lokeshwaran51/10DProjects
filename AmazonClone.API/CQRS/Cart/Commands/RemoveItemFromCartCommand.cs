using MediatR;

namespace AmazonClone.API.CQRS.Cart.Commands
{
    public class RemoveItemFromCartCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public RemoveItemFromCartCommand(int productId)
        {
            ProductId = productId;
        }
    }

}
