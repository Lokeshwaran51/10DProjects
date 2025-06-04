using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.User.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonClone.API.Features.User.CommandHandlers
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
        public async Task<String> Handle(LoginCommand command, CancellationToken token)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == command.Email && u.Password == command.Password, token);

            if (user == null)
                return null;

            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(AmazonClone.API.Data.Entity.User user)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var secretKey = jwtSettings.GetValue<string>("Key");
                var issuer = jwtSettings.GetValue<string>("Issuer");
                var audience = jwtSettings.GetValue<string>("Audience");
                // var expiryMinutes = jwtSettings.GetValue<int>("ExpiryInMinutes");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    //expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to generate JWT token", ex);
            }
        }
    }
}
