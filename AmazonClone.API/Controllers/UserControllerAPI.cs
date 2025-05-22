using AmazonClone.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static AmazonClone.API.Data.Entity.ViewModelAPI;
using static AmazonClone.API.Data.Entity.LoginModel;
using LoginModel = AmazonClone.API.Data.Entity.LoginModel;


namespace AmazonClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllerAPI : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserControllerAPI(AppDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                return Conflict(new { message = "User with this email already exists." });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password." });

            
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token = token,
                //userId = user.Id,
               // userName = user.UserName,
                email = user.Email,
                password=user.Password,
            });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings.GetValue<string>("Key");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryMinutes = jwtSettings.GetValue<int>("ExpiryInMinutes");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //new Claim("userId", user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
