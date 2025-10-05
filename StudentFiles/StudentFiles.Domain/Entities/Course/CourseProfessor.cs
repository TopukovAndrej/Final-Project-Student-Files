namespace StudentFiles.Domain.Entities.Course
{
    using System;

    public class CourseProfessor
    {
        public int Id { get; private set; }

        public string Username { get; private set; }

        public CourseProfessor(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
