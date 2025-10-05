namespace StudentFiles.Application.Queries.Grade
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.Grade;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Database.Context;

    public class GetStudentGradesQuery : IRequest<Result<IReadOnlyList<GradeDto>>>
    {
        public Guid StudentUid { get; }

        public GetStudentGradesQuery(Guid studentUid)
        {
            StudentUid = studentUid;
        }
    }

    public class GetStudentGradesQueryHandler : IRequestHandler<GetStudentGradesQuery, Result<IReadOnlyList<GradeDto>>>
    {
        private readonly IStudentFilesReadonlyDbContext _dbContext;

        public GetStudentGradesQueryHandler(IStudentFilesReadonlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IReadOnlyList<GradeDto>>> Handle(GetStudentGradesQuery query, CancellationToken cancellationToken)
        {
            int studentId = await _dbContext.Users.Where(x => !x.IsDeleted && x.Uid == query.StudentUid).Select(x => x.Id).SingleOrDefaultAsync(cancellationToken);

            if (studentId == default)
            {
                return Result<IReadOnlyList<GradeDto>>.Failed(error: new Error(Code: ErrorCodes.StudentNotFound, Message: ErrorMessage.StudentNotFound),
                                                              resultType: ResultType.NotFound);
            }

            List<GradeDto> studentGrades = await _dbContext.Grades.Where(predicate: x => !x.IsDeleted && x.StudentFk == studentId)
                                                                  .Include(x => x.Course)
                                                                  .Include(x => x.Professor)
                                                                  .Select(selector: x => new GradeDto()
                                                                  {
                                                                      CourseId = x.Course.CourseId,
                                                                      CourseName = x.Course.CourseName,
                                                                      Grade = x.Value,
                                                                      AssignedAt = x.DateAssigned,
                                                                      AssignedBy = (x.Professor == null || x.Professor.IsDeleted) ? string.Empty : x.Professor.Username
                                                                  })
                                                                  .ToListAsync(cancellationToken);

            return Result<IReadOnlyList<GradeDto>>.Success(value: studentGrades);
        }
    }
}
