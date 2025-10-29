using MediatR;
using Microsoft.EntityFrameworkCore;
using UserServices.Constants;
using UserServices.Data.Context;
using UserServices.User.Command;

namespace UserServices.User.CommandHandlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly UserDbContext _context;
        public RegisterHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<String> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                UserServices.Data.Entity.User? userExists = await _context.Users
                       .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);
                if (userExists != null)
                {
                    throw new InvalidOperationException(ResponseMessages.emailExists);
                }
                UserServices.Data.Entity.User newUser = new UserServices.Data.Entity.User
                {
                    UserName = command.UserName,
                    Email = command.Email,
                    Mobile = command.Mobile,
                    Password = command.Password
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(cancellationToken);
                return ResponseMessages.userRegister;
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
