using MediatR;

namespace AmazonClone.API.Features.User.Command
{
    public class RegisterCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
    }
}
