namespace StudentFiles.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using StudentFiles.Api.Common;
    using StudentFiles.Application.Queries.User;
    using StudentFiles.Contracts.Dtos.User;
    using StudentFiles.Contracts.Models.Result;

    [ApiController]
    [Route("/api/users")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("all-non-admin")]
        public async Task<IActionResult> GetAllNonAdminUsersAsync()
        {
            Result<IReadOnlyList<UserDto>> result = await mediator.Send(request: new GetAllNonAdminUsersQuery());

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }
    }
}
