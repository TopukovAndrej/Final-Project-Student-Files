namespace StudentFiles.Domain.Entities.Course
{
    using StudentFiles.Domain.Common.Entities;

    public class Course : AggregateRoot
    {
        public string CourseId { get; private set; }

        public string CourseName { get; private set; }

        public CourseProfessor CourseProfessor { get; private set; }

        private Course(int id, Guid uid, bool isDeleted, string courseId, string courseName, Guid professorUid, string professorUsername)
            : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseProfessor = new CourseProfessor(uid: professorUid, username: professorUsername);
        }
    }
}
