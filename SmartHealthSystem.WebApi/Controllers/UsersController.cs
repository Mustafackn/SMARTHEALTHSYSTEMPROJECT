using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.AppUsers.Commands.CreateUser;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.LoginUser;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            // MediatR aracılığıyla isteği Handler'a gönderiyoruz
            CreateUserCommandResponse response = await _mediator.Send(request);

            if (response.Succeeded)
                return Ok(response);

            return BadRequest(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return Unauthorized(response); // Giriş başarısızsa 401 döner
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] GetUsersQueryRequest request)
        {
            // request.SearchTerm null ise tüm kullanıcılar döner
            var response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
