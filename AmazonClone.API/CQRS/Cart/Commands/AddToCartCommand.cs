using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Cart.Commands
{
    /*public class AddToCartCommand : IRequest<string>
    {
        public string Email { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public AddToCartCommand(string email, int productId, int quantity)
        {
            Email = email;
            ProductId = productId;
            Quantity = quantity;
        }
    }*/

    public class AddToCartCommand : IRequest<string>
    {
        public AddToCartRequestDTO Request;
        public AddToCartCommand(AddToCartRequestDTO request)
        {
            Request = request;            
        }
    }

}
