namespace StudentFiles.Application.Commands.User
{
    using MediatR;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Domain.Entities.Grade;
    using StudentFiles.Domain.Entities.User;
    using StudentFiles.Infrastructure.Data.Repositories.Course;
    using StudentFiles.Infrastructure.Data.Repositories.Grade;
    using StudentFiles.Infrastructure.Data.Repositories.User;

    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid UserUid { get; set; }

        public DeleteUserCommand(Guid userUid)
        {
            UserUid = userUid;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IGradeRepository _gradeRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository,
                                        ICourseRepository courseRepository,
                                        IGradeRepository gradeRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken = default)
        {
            Result<User> userResult = await _userRepository.GetUserByUidAsync(userUid: command.UserUid);

            if (userResult.IsFailure)
            {
                return Result.Failed(error: userResult.Error, resultType: userResult.Type);
            }

            var user = userResult.Value!;

            if (user.Role.Code == Domain.Entities.User.UserRole.Professor.Code)
            {
                bool professorHasCourses = await _courseRepository.CheckIfProfessorHasCoursesAsync(professorId: user.Id);

                if (professorHasCourses)
                {
                    return Result.Failed(error: new Error(Code: ErrorCodes.ProfessorHasCourses, Message: ErrorMessage.ProfessorHasCourses), resultType: ResultType.Forbidden);
                }
            }
            else if (user.Role.Code == Domain.Entities.User.UserRole.Student.Code)
            {
                Result<IReadOnlyList<Grade>> studentGradesResult = await _gradeRepository.GetAllGradesForStudentIdAsync(studentId: user.Id);

                if (studentGradesResult.IsFailure)
                {
                    return Result.Failed(error: studentGradesResult.Error, resultType: studentGradesResult.Type);
                }

                foreach (var grade in studentGradesResult.Value!)
                {
                    grade.MarkDelete();

                    _gradeRepository.UpdateGrade(grade);
                }
            }
            else
            {
                return Result.Failed(error: new Error(Code: ErrorCodes.AdminDeleteForbidden, Message: ErrorMessage.ProfessorHasCourses), resultType: ResultType.Forbidden);
            }
            
            await _gradeRepository.SaveChangesAsync();

            user.MarkDelete();

            _userRepository.UpdateUser(user);

            await _userRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
