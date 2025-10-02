namespace StudentFiles.Application.Queries.User
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.User;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Database.Context;

    public class GetUserQuery : IRequest<Result<UserDto>>
    {
        public Guid UserUid { get; }

        public GetUserQuery(Guid userUid)
        {
            UserUid = userUid;
        }
    }

    public class GetUserQueryHandler(IStudentFilesReadonlyDbContext dbContext) : IRequestHandler<GetUserQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            UserDto? userDto = await dbContext.Users.Where(predicate: x => !x.IsDeleted && x.Uid == query.UserUid)
                                                    .Select(selector: x => new UserDto() { Uid = x.Uid, Username = x.Username, Role = x.Role })
                                                    .SingleOrDefaultAsync(cancellationToken);

            if (userDto is null)
            {
                return Result<UserDto>.Failed(error: new Error(Code: ErrorCodes.UserNotFound, Message: ErrorMessage.UserNotFound),
                                              resultType: ResultType.NotFound);
            }

            return Result<UserDto>.Success(value: userDto);
        }
    }
}
