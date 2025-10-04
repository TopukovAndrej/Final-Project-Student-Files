namespace StudentFiles.Infrastructure.Data.Models
{
    public class Course
    {
        public int Id { get; internal set; }

        public Guid Uid { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public string CourseId { get; internal set; }

        public string CourseName { get; internal set; }

        public int ProfessorFk { get; internal set; }

        public virtual User Professor { get; internal set; }

        private Course() { }

        public Course(int id, Guid uid, bool isDeleted, string courseId, string courseName, int professorFk)
        {
            Id = id;
            Uid = uid;
            IsDeleted = isDeleted;
            CourseId = courseId;
            CourseName = courseName;
            ProfessorFk = professorFk;
        }
    }
}
