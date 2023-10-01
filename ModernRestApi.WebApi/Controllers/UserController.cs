using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModernRestApi.Application.Features.UserFeatures.CreateUser;
using ModernRestApi.Application.Features.UserFeatures.DeleteUser;
using ModernRestApi.Application.Features.UserFeatures.GetAllUser;
using ModernRestApi.Application.Features.UserFeatures.UpdateUser;

namespace ModernRestApi.WebApi.Controllers
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<UpdateUserResponse>> Update(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<DeleteUserResponse>> Delete(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}