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

    public class GetAllUsersQuery : IRequest<Result<IReadOnlyList<UserDto>>>
    {
        public GetAllUsersQuery() { }
    }

    public class GetAllUsersQueryHandler(IStudentFilesReadonlyDbContext dbContext) : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<UserDto>>>
    {
        public async Task<Result<IReadOnlyList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserDto> users = await dbContext.Users.Where(predicate: x => !x.IsDeleted)
                                                       .Select(selector: x => new UserDto() { Username = x.Username, Role = x.Role })
                                                       .ToListAsync(cancellationToken: cancellationToken);

            if (users.Count == 0)
            {
                return Result<IReadOnlyList<UserDto>>.Failed(errorMessage: ErrorCodes.UsersNotFound, resultType: ResultType.NotFound);
            }

            return Result<IReadOnlyList<UserDto>>.Success(value: users);
        }
    }
}
