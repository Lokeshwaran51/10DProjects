using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserServices.User.Command;

namespace UserServices.Controllers
{
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
                string res = await _mediator.Send(command);
                return Ok(res);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Internal Server Error.");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                string token = await _mediator.Send(command);

                if (token == null)
                    return Unauthorized("Invalid credentials");
                return Ok(new { Token = token });
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Internal Server Error.");
            }
        }
    }
}
