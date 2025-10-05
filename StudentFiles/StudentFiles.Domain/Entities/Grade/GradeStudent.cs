namespace StudentFiles.Domain.Entities.Grade
{
    public class GradeStudent
    {
        public int Id { get; private set; }

        public string Username { get; private set; }

        public GradeStudent(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
