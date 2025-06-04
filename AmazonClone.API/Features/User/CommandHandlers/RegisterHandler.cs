using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.User.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.API.Features.User.CommandHandlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly AppDbContext _context;
        public RegisterHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<String> Handle(RegisterCommand command, CancellationToken token)
        {
            var userExists = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == command.Email);
            if (userExists != null)
            {
                throw new Exception("User with this email already exists.");
            }
            var newUser = new AmazonClone.API.Data.Entity.User
            {
                UserName = command.UserName,
                Email = command.Email,
                Mobile = command.Mobile,
                Password = command.Password
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(token);
            return "User Registered Successfully.";
        }
    }
}
