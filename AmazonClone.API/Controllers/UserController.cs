using AmazonClone.API.Features.User.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AmazonClone.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*[AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }*/

        /*[AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login model)
        {
            try
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
                    email = user.Email,
                    password = user.Password,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }*/

        /* private string GenerateJwtToken(User user)
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
         }*/

    }
}
