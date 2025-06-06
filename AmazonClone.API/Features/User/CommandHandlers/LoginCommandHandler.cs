using AmazonClone.API.Constants;
using AmazonClone.API.Data.Entity;
using AmazonClone.API.Features.User.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users = AmazonClone.API.Data.Entity.User;

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
            Users user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == command.Email && u.Password == command.Password, token);

            if (user == null)
                return null;

            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(AmazonClone.API.Data.Entity.User user)
        {
            try
            {
                IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
                string secretKey = jwtSettings.GetValue<string>("Key");
                string issuer = jwtSettings.GetValue<string>("Issuer");
                string audience = jwtSettings.GetValue<string>("Audience");
                // var expiryMinutes = jwtSettings.GetValue<int>("ExpiryInMinutes");

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                Claim[] claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                JwtSecurityToken token = new JwtSecurityToken(
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
                throw new ApplicationException(ResponseMessages.failToLoadJWT,ex);
            }
        }
    }
}
