namespace StudentFiles.Domain.Entities.Grade
{
    public class GradeCourse
    {
        public int Id { get; private set; }

        public string CourseId { get; private set; }

        public string CourseName { get; private set; }

        public GradeCourse(int id, string courseId, string courseName)
        {
            Id = id;
            CourseId = courseId;
            CourseName = courseName;
        }
    }
}
