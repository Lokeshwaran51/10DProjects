using CartServices.Data.DTOs;
using MediatR;

namespace CartServices.Cart.Commands
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
