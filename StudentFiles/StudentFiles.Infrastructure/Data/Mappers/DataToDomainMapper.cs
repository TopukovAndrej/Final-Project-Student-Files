namespace StudentFiles.Infrastructure.Data.Mappers
{
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Data.Models;

    public class DataToDomainMapper
    {
        public static Result<Domain.Entities.User.User> MapUserDataToDomain(User dbUser)
        {
            Result<Domain.Entities.User.User> domainUserResult = Domain.Entities.User.User.Create(id: dbUser.Id,
                                                                                                  uid: dbUser.Uid,
                                                                                                  isDeleted: dbUser.IsDeleted,
                                                                                                  username: dbUser.Username,
                                                                                                  hashedPassword: dbUser.HashedPassword,
                                                                                                  role: dbUser.Role);

            return domainUserResult;
        }

        public static Result<Domain.Entities.Course.Course> MapCourseDataToDomain(Course dbCourse)
        {
            Result<Domain.Entities.Course.Course> domainCourseResult = Domain.Entities.Course.Course.Create(id: dbCourse.Id,
                                                                                                            uid: dbCourse.Uid,
                                                                                                            isDeleted: dbCourse.IsDeleted,
                                                                                                            courseId: dbCourse.CourseId,
                                                                                                            courseName: dbCourse.CourseName,
                                                                                                            professorId: dbCourse.Professor.Id,
                                                                                                            professorUsername: dbCourse.Professor.Username);

            return domainCourseResult;
        }

        public static Result<Domain.Entities.Grade.Grade> MapGradeDataToDomain(Grade dbGrade)
        {
            Result<Domain.Entities.Grade.Grade> domainGradeResult = Domain.Entities.Grade.Grade.Create(id: dbGrade.Id,
                                                                                                       uid: dbGrade.Uid,
                                                                                                       isDeleted: dbGrade.IsDeleted,
                                                                                                       value: dbGrade.Value,
                                                                                                       dateAssigned: dbGrade.DateAssigned,
                                                                                                       studentId: dbGrade.Student.Id,
                                                                                                       studentUsername: dbGrade.Student.Username,
                                                                                                       courseId: dbGrade.Course.Id,
                                                                                                       courseCode: dbGrade.Course.CourseId,
                                                                                                       courseName: dbGrade.Course.CourseName,
                                                                                                       professorId: dbGrade.Professor.Id,
                                                                                                       professorUsername: dbGrade.Professor.Username);

            return domainGradeResult;
        }
    }
}
