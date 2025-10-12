namespace StudentFiles.Application.Queries.User
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.User;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Database.Context;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllNonAdminUsersQuery : IRequest<Result<IReadOnlyList<UserDto>>>
    {
        public GetAllNonAdminUsersQuery() { }
    }

    public class GetAllNonAdminUsersQueryHandler(IStudentFilesReadonlyDbContext dbContext) : IRequestHandler<GetAllNonAdminUsersQuery, Result<IReadOnlyList<UserDto>>>
    {
        public async Task<Result<IReadOnlyList<UserDto>>> Handle(GetAllNonAdminUsersQuery query, CancellationToken cancellationToken)
        {
            List<UserDto> users = await dbContext.Users.Where(predicate: x => !x.IsDeleted && x.Role != Domain.Entities.User.UserRole.Admin.Code)
                                                       .Select(selector: x => new UserDto() { Uid = x.Uid, Username = x.Username, Role = x.Role })
                                                       .ToListAsync(cancellationToken: cancellationToken);

            if (users.Count == 0)
            {
                return Result<IReadOnlyList<UserDto>>.Failed(error: new Error(Code: ErrorCodes.UsersNotFound, Message: ErrorMessage.UsersNotFound),
                                                             resultType: ResultType.NotFound);
            }

            return Result<IReadOnlyList<UserDto>>.Success(value: users);
        }
    }
}
