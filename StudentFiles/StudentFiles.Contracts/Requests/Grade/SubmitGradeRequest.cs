namespace StudentFiles.Contracts.Requests.Grade
{
    public class SubmitGradeRequest
    {
        public Guid StudentUid { get; }

        public Guid CourseUid { get; }

        public Guid ProfessorUid { get; }

        public int Grade { get; }

        public SubmitGradeRequest(Guid studentUid, Guid courseUid, Guid professorUid, int grade)
        {
            StudentUid = studentUid;
            CourseUid = courseUid;
            ProfessorUid = professorUid;
            Grade = grade;
        }
    }
}
