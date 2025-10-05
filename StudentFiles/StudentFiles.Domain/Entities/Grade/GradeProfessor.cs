namespace StudentFiles.Domain.Entities.Grade
{
    public class GradeProfessor
    {
        public int Id { get; private set; }

        public string Username { get; private set; }

        public GradeProfessor(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
