namespace StudentFiles.Domain.Entities.Professor
{
    using StudentFiles.Domain.Common.Entities;

    public class Professor : AggregateRoot
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int ProfessorId { get; private set; }

        public string Department { get; private set; }

        // public IReadOnlyList<Course> CoursesTaught { get; private set; } TO BE DONE IN ANOTHER PR

        private Professor(int id,
                          Guid uid,
                          bool isDeleted,
                          string firstName,
                          string lastName,
                          int professorId,
                          string department) : base(id, uid, isDeleted)
                          //* IReadOnlyList<Course> coursesTaught *//
        {
            FirstName = firstName;
            LastName = lastName;
            ProfessorId = professorId;
            Department = department;
            // CoursesTaught = coursesTaught;
        }
    }
}
