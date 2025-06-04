using MediatR;

namespace AmazonClone.API.Features.User.Command
{
    public class LoginCommand : IRequest<String>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
