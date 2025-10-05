namespace StudentFiles.Application.Commands.Grade
{
    using MediatR;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.Grade;
    using StudentFiles.Domain.Entities.Course;
    using StudentFiles.Domain.Entities.Grade;
    using StudentFiles.Domain.Entities.User;
    using StudentFiles.Infrastructure.Data.Repositories.Course;
    using StudentFiles.Infrastructure.Data.Repositories.Grade;
    using StudentFiles.Infrastructure.Data.Repositories.User;

    public class SubmitGradeCommand : IRequest<Result>
    {
        public SubmitGradeRequest Request { get; set; }

        public SubmitGradeCommand(SubmitGradeRequest request)
        {
            Request = request;
        }
    }

    public class SubmitGradeCommandHandler : IRequestHandler<SubmitGradeCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IGradeRepository _gradeRepository;

        public SubmitGradeCommandHandler(IUserRepository userRepository, ICourseRepository courseRepository, IGradeRepository gradeRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<Result> Handle(SubmitGradeCommand command, CancellationToken cancellationToken = default)
        {
            if (!IsGradeValueValid(gradeValue: command.Request.Grade))
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.GradeNotValid, Message: ErrorMessage.GradeNotValid), resultType: ResultType.BadRequest);
            }

            Result<Course> courseResult = await _courseRepository.GetCourseByUidAsync(uid: command.Request.CourseUid);

            if (courseResult.IsFailure)
            {
                return Result.Failed(error: courseResult.Error, resultType: courseResult.Type);
            }

            Result<User> professorResult = await _userRepository.GetUserByUidAsync(userUid: command.Request.ProfessorUid);

            if (professorResult.IsFailure)
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.ProfessorNotFound, Message: ErrorMessage.ProfessorNotFound), resultType: ResultType.NotFound);
            }

            if (courseResult.Value!.CourseProfessor.Id != professorResult.Value!.Id)
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.ProfessorCourseGradingNotValid, Message: ErrorMessage.ProfessorCourseGradingNotValid), resultType: ResultType.Invalid);
            }

            Result<User> studentResult = await _userRepository.GetUserByUidAsync(userUid: command.Request.StudentUid);

            if (studentResult.IsFailure)
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.StudentNotFound, Message: ErrorMessage.StudentNotFound), resultType: ResultType.NotFound);
            }

            bool hasStudentGraduated = await _gradeRepository.CheckIfStudentIsGraduatedAsync(studentId: studentResult.Value!.Id);

            if (hasStudentGraduated)
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.StudentAlreadyGraduated, Message: ErrorMessage.StudentAlreadyGraduated), resultType: ResultType.Invalid);
            }

            bool hasStudentAlreadyBeenGradedForSelectedCourse = await _gradeRepository.CheckIfStudentAlreadyHasGradeForCourseAsync(studentId: studentResult.Value!.Id,
                                                                                                                                   courseId: courseResult.Value!.Id);

            if (hasStudentAlreadyBeenGradedForSelectedCourse)
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.StudentAlreadyGradedForCourse, Message: ErrorMessage.StudentAlreadyGradedForCourse), resultType: ResultType.Invalid);
            }

            Result<Grade> gradeResult = Grade.Create(id: 0,
                                                     uid: Guid.NewGuid(),
                                                     isDeleted: false,
                                                     value: command.Request.Grade,
                                                     dateAssigned: DateOnly.FromDateTime(DateTime.UtcNow),
                                                     studentId: studentResult.Value!.Id,
                                                     studentUsername: studentResult.Value!.Username,
                                                     professorId: professorResult.Value!.Id,
                                                     professorUsername: professorResult.Value!.Username,
                                                     courseId: courseResult.Value.Id,
                                                     courseCode: courseResult.Value.CourseId,
                                                     courseName: courseResult.Value.CourseName);

            if (gradeResult.IsFailure)
            {
                return Result.Failed(error: gradeResult.Error, resultType: gradeResult.Type);
            }

            await _gradeRepository.InsertGradeAsync(grade: gradeResult.Value!);

            await _gradeRepository.SaveChangesAsync();

            return Result.Success();
        }

        private static bool IsGradeValueValid(int gradeValue)
        {
            return gradeValue >= 6 && gradeValue <= 10;
        }
    }
}
