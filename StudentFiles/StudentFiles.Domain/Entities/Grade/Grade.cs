namespace StudentFiles.Domain.Entities.Grade
{
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Domain.Common.Entities;

    public class Grade : Entity
    {
        public int Value { get; private set; }

        public DateOnly DateAssigned { get; private set; }

        public GradeStudent Student { get; private set; }

        public GradeCourse Course { get; private set; }

        public GradeProfessor Professor { get; private set; }

        private Grade(int id,
                      Guid uid,
                      bool isDeleted,
                      int value,
                      DateOnly dateAssigned,
                      int studentId,
                      string studentUsername,
                      int courseId,
                      string courseCode,
                      string courseName,
                      int professorId,
                      string professorUsername) : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            Value = value;
            DateAssigned = dateAssigned;
            Student = new GradeStudent(id: studentId, username: studentUsername);
            Course = new GradeCourse(id: courseId, courseId: courseCode, courseName: courseName);
            Professor = new GradeProfessor(id: professorId, username: professorUsername);
        }

        public static Result<Grade> Create(int id,
                                           Guid uid,
                                           bool isDeleted,
                                           int value,
                                           DateOnly dateAssigned,
                                           int studentId,
                                           string studentUsername,
                                           int courseId,
                                           string courseCode,
                                           string courseName,
                                           int professorId,
                                           string professorUsername)
        {
            if (!IsValueValid(value))
            {
                return Result<Grade>.Failed(error: new Error(Code: ErrorCodes.GradeNotValid, Message: ErrorMessage.GradeNotValid), resultType: ResultType.Invalid);
            }

            return Result<Grade>.Success(new Grade(id: id,
                                                   uid: uid,
                                                   isDeleted: isDeleted,
                                                   value: value,
                                                   dateAssigned: dateAssigned,
                                                   studentId: studentId,
                                                   studentUsername: studentUsername,
                                                   courseId: courseId,
                                                   courseCode: courseCode,
                                                   courseName: courseName,
                                                   professorId: professorId,
                                                   professorUsername: professorUsername));
        }

        private static bool IsValueValid(int value)
        {
            return value >= 6 && value <= 10;
        }

        public void MarkDelete()
        {
            IsDeleted = true;
        }
    }
}
