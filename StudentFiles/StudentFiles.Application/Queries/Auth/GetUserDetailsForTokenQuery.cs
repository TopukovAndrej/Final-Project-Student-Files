namespace StudentFiles.Application.Queries.Auth
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.Auth;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Database.Context;

    public class GetUserDetailsForTokenQuery : IRequest<Result<UserDetailsForTokenDto>>
    {
        public int UserId { get; }

        public GetUserDetailsForTokenQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserDetailsForTokenQueryHandler(IStudentFilesReadonlyDbContext dbContext) : IRequestHandler<GetUserDetailsForTokenQuery, Result<UserDetailsForTokenDto>>
    {
        public async Task<Result<UserDetailsForTokenDto>> Handle(GetUserDetailsForTokenQuery query, CancellationToken cancellationToken)
        {
            UserDetailsForTokenDto? userDetailsForTokenDto = await dbContext.Users.Where(predicate: x => !x.IsDeleted && x.Id == query.UserId)
                                                                                  .Select(selector: x => new UserDetailsForTokenDto() { UserUid = x.Uid, Username = x.Username, UserRole = x.Role })
                                                                                  .SingleOrDefaultAsync(cancellationToken);

            if (userDetailsForTokenDto is null)
            {
                return Result<UserDetailsForTokenDto>.Failed(error: new Error(Code: ErrorCodes.UserNotFound, Message: ErrorMessage.UserNotFound),
                                                             resultType: ResultType.Unauthorized);
            }

            return Result<UserDetailsForTokenDto>.Success(value: userDetailsForTokenDto);
        }
    }
}
