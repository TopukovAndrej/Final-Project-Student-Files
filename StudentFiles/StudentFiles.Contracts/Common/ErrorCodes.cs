namespace StudentFiles.Contracts.Common
{
    public static class ErrorCodes
    {
        // Auth
        public static readonly string UserCredentialsNotValid = "USER_CREDENTIALS_NOT_VALID";

        // Users
        public static readonly string UserNotFound = "USER_NOT_FOUND";
        public static readonly string UserRoleNotValid = "USER_ROLE_NOT_VALID";
        public static readonly string UsersNotFound = "USERS_NOT_FOUND";
        public static readonly string UserEmailNotValid = "USER_EMAIL_NOT_VALID";
        public static readonly string UserPasswordNotValid = "USER_PASSWORD_NOT_VALID";
        public static readonly string UserAlreadyExists = "USER_ALREADY_EXISTS";
        public static readonly string ProfessorNotFound = "PROFESSOR_NOT_FOUND";
        public static readonly string StudentsNotFound = "STUDENTS_NOT_FOUND";
        public static readonly string StudentNotFound = "STUDENT_NOT_FOUND";
        public static readonly string StudentAlreadyGraduated = "STUDENT_ALREADY_GRADUATED";
        public static readonly string StudentAlreadyGradedForCourse = "STUDENT_ALREADY_GRADED_FOR_COURSE";
        public static readonly string AdminDeleteForbidden = "ADMIN_DELETE_FORBIDDEN";

        // Courses
        public static readonly string CourseNotFound = "COURSE_NOT_FOUND";
        public static readonly string ProfessorCourseGradingNotValid = "PROFESSOR_COURSE_GRADING_NOT_VALID";
        public static readonly string ProfessorHasCourses = "PROFESSOR_HAS_COURSES";

        // Grades
        public static readonly string GradeNotValid = "GRADE_NOT_VALID";
    }
}
