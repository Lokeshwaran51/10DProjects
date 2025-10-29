using MediatR;

namespace UserServices.User.Command
{
    public class LoginCommand:IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
