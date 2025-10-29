using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Cart.Commands
{
    public class AddToCartCommand : IRequest<string>
    {
        public AddToCartRequestDto Request { get; }

        public AddToCartCommand(AddToCartRequestDto request)
        {
            Request = request;
        }
    }
}
