namespace StudentFiles.Domain.Entities.Course
{
    using System;

    public class CourseProfessor
    {
        public Guid Uid { get; private set; }

        public string Username { get; private set; }

        public CourseProfessor(Guid uid, string username)
        {
            Uid = uid;
            Username = username;
        }
    }
}
