using MediatR;

namespace CartServices.Cart.Commands
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
