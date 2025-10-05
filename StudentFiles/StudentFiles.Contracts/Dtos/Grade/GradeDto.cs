namespace StudentFiles.Contracts.Dtos.Grade
{
    public class GradeDto
    {
        public string CourseId { get; init; }

        public string CourseName { get; init; }

        public int Grade { get; init; }

        public DateOnly AssignedAt { get; init; }

        public string AssignedBy { get; init; }
    }
}
