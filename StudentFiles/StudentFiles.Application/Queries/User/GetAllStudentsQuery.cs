namespace StudentFiles.Application.Queries.User
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.User;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Database.Context;

    public class GetAllStudentsQuery : IRequest<Result<IReadOnlyList<SimpleUserDto>>>
    {
        public GetAllStudentsQuery() { }
    }

    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, Result<IReadOnlyList<SimpleUserDto>>>
    {
        private readonly IStudentFilesReadonlyDbContext _dbContext;

        public GetAllStudentsQueryHandler(IStudentFilesReadonlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IReadOnlyList<SimpleUserDto>>> Handle(GetAllStudentsQuery query, CancellationToken cancellationToken)
        {
            List<SimpleUserDto> students = await _dbContext.Users.Where(predicate: x => !x.IsDeleted && x.Role == Domain.Entities.User.UserRole.Student.Code)
                                                                 .Select(selector: x => new SimpleUserDto() { Uid = x.Uid, Username = x.Username })
                                                                 .ToListAsync(cancellationToken: cancellationToken);

            if (students.Count == 0)
            {
                return Result<IReadOnlyList<SimpleUserDto>>.Failed(error: new Error(Code: ErrorCodes.StudentsNotFound, Message: ErrorMessage.StudentsNotFound),
                                                                   resultType: ResultType.NotFound);
            }

            return Result<IReadOnlyList<SimpleUserDto>>.Success(value: students);
        }
    }
}
