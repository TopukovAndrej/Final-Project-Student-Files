namespace StudentFiles.Domain.Entities.Course
{
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Domain.Common.Entities;

    public class Course : AggregateRoot
    {
        public string CourseId { get; private set; }

        public string CourseName { get; private set; }

        public CourseProfessor CourseProfessor { get; private set; }

        private Course(int id, Guid uid, bool isDeleted, string courseId, string courseName, int professorId, string professorUsername)
            : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseProfessor = new CourseProfessor(id: professorId, username: professorUsername);
        }

        public static Result<Course> Create(int id, Guid uid, bool isDeleted, string courseId, string courseName, int professorId, string professorUsername)
        {
            return Result<Course>.Success(new Course(id: id,
                                                     uid: uid,
                                                     isDeleted: isDeleted,
                                                     courseId: courseId,
                                                     courseName: courseName,
                                                     professorId: professorId,
                                                     professorUsername: professorUsername));
        }
    }
}
