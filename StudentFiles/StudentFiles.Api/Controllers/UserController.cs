namespace StudentFiles.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentFiles.Api.Common;
    using StudentFiles.Application.Commands.User;
    using StudentFiles.Application.Queries.User;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.User;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.User;

    [ApiController]
    [Route("/api/users")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        [Route("all-non-admin")]
        public async Task<IActionResult> GetAllNonAdminUsersAsync()
        {
            Result<IReadOnlyList<UserDto>> result = await mediator.Send(request: new GetAllNonAdminUsersQuery());

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }

        [HttpDelete]
        [Authorize(Roles = UserRole.Admin)]
        [Route("delete-user/{userUid:guid}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userUid)
        {
            Result result = await mediator.Send(request: new DeleteUserCommand(userUid: userUid));

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        [Route("create-user")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            Result<Guid> result = await mediator.Send(request: new CreateUserCommand(request: request));

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        [Route("get-user/{userUid:guid}")]
        public async Task<IActionResult> GetUserByUidAsync([FromRoute] Guid userUid)
        {
            Result<UserDto> result = await mediator.Send(request: new GetUserQuery(userUid: userUid));

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Professor)]
        [Route("all-students")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            Result<IReadOnlyList<SimpleUserDto>> result = await mediator.Send(request: new GetAllStudentsQuery());

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }
    }
}
