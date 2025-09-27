namespace StudentFiles.Application.Queries.Auth
{
    using BCrypt.Net;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.Auth;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.Auth;
    using StudentFiles.Infrastructure.Database.Context;

    public class AuthenticateUserQuery : IRequest<Result<int>>
    {
        public UserLoginRequest LoginRequest { get; }

        public AuthenticateUserQuery(UserLoginRequest userLoginRequest)
        {
            LoginRequest = userLoginRequest;
        }
    }

    public class AuthenticateUserQueryHandler(IStudentFilesReadonlyDbContext dbContext) : IRequestHandler<AuthenticateUserQuery, Result<int>>
    {
        public async Task<Result<int>> Handle(AuthenticateUserQuery query, CancellationToken cancellationToken)
        {
            UserAuthDto? userAuthDto = await dbContext.Users.Where(predicate: x => !x.IsDeleted && x.Username == query.LoginRequest.UserName)
                                                            .Select(selector: x => new UserAuthDto() { UserId = x.Id, Username = x.Username, HashedPassword = x.HashedPassword })
                                                            .SingleOrDefaultAsync(cancellationToken);

            if (userAuthDto is not null && BCrypt.Verify(text: query.LoginRequest.Password, hash: userAuthDto.HashedPassword))
            {
                return Result<int>.Success(value: userAuthDto.UserId);
            }

            return Result<int>.Failed(error: new Error(Code: ErrorCodes.UserCredentialsNotValid, Message: ErrorMessage.UserCredentialsNotValid),
                                      resultType: ResultType.Unauthorized);
        }
    }
}
