using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.CQRS.User.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users = AmazonClone.API.Data.Entity.User;

namespace AmazonClone.API.CQRS.User.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, String>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<String> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Users? user = await _context.Users
                       .FirstOrDefaultAsync(u => u.Email == command.Email && u.Password == command.Password, cancellationToken);

                if (user == null)
                    throw new InvalidOperationException(ResponseMessages.userNotFound);

                return GenerateJwtToken(user);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
        private string GenerateJwtToken(Users user)
        {
            try
            {
                IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
                string? secretKey = jwtSettings.GetValue<string>("Key");
                string? issuer = jwtSettings.GetValue<string>("Issuer");
                string? audience = jwtSettings.GetValue<string>("Audience");
                int expiryMinutes = jwtSettings.GetValue<int>("ExpiryInMinutes");

                if (string.IsNullOrEmpty(secretKey))
                {
                    throw new InvalidOperationException(ResponseMessages.secretKeyCntBeNull);
                }

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                Claim[] claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(ResponseMessages.internalServerErrorMessage);
            }
        }
    }
}
