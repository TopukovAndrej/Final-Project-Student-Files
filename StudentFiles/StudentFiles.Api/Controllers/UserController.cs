namespace StudentFiles.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using StudentFiles.Api.Common;
    using StudentFiles.Application.Queries.User;
    using StudentFiles.Contracts.Dtos.User;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.User;

    [ApiController]
    [Route("/api/users")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            Result<IReadOnlyList<UserDto>> result = await mediator.Send(request: new GetAllUsersQuery());

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }

        [HttpDelete]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUserAsync([FromBody] DeleteUserRequest request)
        {
            Result result = await mediator.Send(request: new DeleteUserCommand(request: request));

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }
    }
}
