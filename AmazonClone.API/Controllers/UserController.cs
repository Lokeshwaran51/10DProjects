using AmazonClone.API.CQRS.User.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.API.Controllers
{
    //[Authorize]
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
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);

            if (token == null)
                return Unauthorized("Invalid credentials");
            return Ok(new { Token = token });
        }
    }
}
