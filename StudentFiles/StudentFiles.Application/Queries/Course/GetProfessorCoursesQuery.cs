namespace StudentFiles.Application.Queries.Course
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.Course;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Database.Context;

    public class GetProfessorCoursesQuery : IRequest<Result<IReadOnlyList<CourseDto>>>
    {
        public Guid ProfessorUid { get; }

        public GetProfessorCoursesQuery(Guid professorUid)
        {
            ProfessorUid = professorUid;
        }
    }

    public class GetProfessorCoursesQueryHandler : IRequestHandler<GetProfessorCoursesQuery, Result<IReadOnlyList<CourseDto>>>
    {
        private readonly IStudentFilesReadonlyDbContext _dbContext;

        public GetProfessorCoursesQueryHandler(IStudentFilesReadonlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IReadOnlyList<CourseDto>>> Handle(GetProfessorCoursesQuery query, CancellationToken cancellationToken)
        {
            int professorId = await _dbContext.Users.Where(predicate: x => x.Uid == query.ProfessorUid)
                                                    .Select(selector: x => x.Id)
                                                    .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (professorId == 0)
            {
                return Result<IReadOnlyList<CourseDto>>.Failed(error: new Error(Code: ErrorCodes.ProfessorNotFound, Message: ErrorMessage.ProfessorNotFound),
                                                               resultType: ResultType.NotFound);
            }

            List<CourseDto> courses = await _dbContext.Courses.Where(predicate: x => !x.IsDeleted && x.ProfessorFk == professorId)
                                                              .Select(selector: x => new CourseDto() { Uid = x.Uid, CourseId = x.CourseId, CourseName = x.CourseName })
                                                              .ToListAsync(cancellationToken: cancellationToken);

            return Result<IReadOnlyList<CourseDto>>.Success(value: courses);
        }
    }
}
