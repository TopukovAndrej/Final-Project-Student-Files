namespace StudentFiles.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using StudentFiles.Api.Common;
    using StudentFiles.Api.Services;
    using StudentFiles.Application.Queries.Auth;
    using StudentFiles.Contracts.Dtos.Auth;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.Auth;

    [ApiController]
    [Route("/api/auth")]
    public class AuthController(IMediator _mediator, IJwtService _jwtService) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] UserLoginRequest request)
        {
            Result<int> authenticateUserQueryResult = await _mediator.Send(request: new AuthenticateUserQuery(userLoginRequest: request));

            if (authenticateUserQueryResult.IsFailure)
            {
                return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: authenticateUserQueryResult.Type),
                                  value: authenticateUserQueryResult);
            }

            Result<UserDetailsForTokenDto> userDetailsForTokenQueryResult = await _mediator.Send(request: new GetUserDetailsForTokenQuery(userId: authenticateUserQueryResult.Value));

            if (userDetailsForTokenQueryResult.IsFailure)
            {
                return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: userDetailsForTokenQueryResult.Type),
                                  value: userDetailsForTokenQueryResult);
            }

            string jsonWebToken;

            try
            {
                jsonWebToken = _jwtService.GenerateJwtToken(userUid: userDetailsForTokenQueryResult.Value!.UserUid,
                                                            userName: userDetailsForTokenQueryResult.Value.Username,
                                                            userRole: userDetailsForTokenQueryResult.Value.UserRole);
            }
            catch (Exception)
            {
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, value: "Failed to authenticate user. Please contact support. Code: 500");
            }

            return Ok(value: new { token = jsonWebToken });
        }
    }
}
