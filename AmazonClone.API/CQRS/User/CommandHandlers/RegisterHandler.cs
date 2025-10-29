using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.CQRS.User.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users = AmazonClone.API.Data.Entity.User;

namespace AmazonClone.API.Features.User.CommandHandlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly AppDbContext _context;
        public RegisterHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<String> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Users? userExists = await _context.Users
                       .FirstOrDefaultAsync(u => u.Email == command.Email,cancellationToken);
                if (userExists != null)
                {
                    throw new InvalidOperationException(ResponseMessages.emailExists);
                }
                Users newUser = new Users
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
