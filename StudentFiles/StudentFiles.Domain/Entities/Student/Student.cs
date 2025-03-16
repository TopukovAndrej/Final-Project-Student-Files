namespace StudentFiles.Domain.Entities.Student
{
    using StudentFiles.Domain.Common.Entities;

    public class Student : AggregateRoot
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateOnly DateOfBirth { get; private set; }

        public string IndexNumber { get; private set; }

        public decimal Gpa { get; private set; }

        public string PhoneNumber { get; private set; }

        public int EnrollmentYear { get; private set; }

        public StudentStatus Status { get; private set; }

        private Student(int id,
                        Guid uid,
                        bool isDeleted,
                        string firstName,
                        string lastName,
                        DateOnly dateOfBirth,
                        string indexNumber,
                        decimal gpa,
                        string phoneNumber,
                        int enrollmentYear,
                        StudentStatus status) : base(id, uid, isDeleted)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            IndexNumber = indexNumber;
            Gpa = gpa;
            PhoneNumber = phoneNumber;
            EnrollmentYear = enrollmentYear;
            Status = status;
        }
    }
}
