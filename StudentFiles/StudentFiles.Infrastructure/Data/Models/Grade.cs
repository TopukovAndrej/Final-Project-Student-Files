namespace StudentFiles.Infrastructure.Data.Models
{
    public class Grade
    {
        public int Id { get; internal set; }

        public Guid Uid { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public int Value { get; internal set; }

        public DateOnly DateAssigned { get; internal set; }

        public int StudentFk { get; internal set; }

        public int CourseFk { get; internal set; }

        public int ProfessorFk { get; internal set; }

        public virtual User Student { get; internal set; }

        public virtual Course Course { get; internal set; }

        public virtual User Professor { get; internal set; }

        public Grade(int id,
                     Guid uid,
                     bool isDeleted,
                     int value,
                     DateOnly dateAssigned,
                     int studentFk,
                     int courseFk,
                     int professorFk)
        {
            Id = id;
            Uid = uid;
            IsDeleted = isDeleted;
            Value = value;
            DateAssigned = dateAssigned;
            StudentFk = studentFk;
            CourseFk = courseFk;
            ProfessorFk = professorFk;
        }
    }
}
