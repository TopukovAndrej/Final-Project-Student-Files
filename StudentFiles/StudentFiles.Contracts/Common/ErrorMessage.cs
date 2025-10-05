namespace StudentFiles.Contracts.Common
{
    public static class ErrorMessage
    {
        // Auth
        public static readonly string UserCredentialsNotValid = "Invalid username or password.";

        // Users
        public static readonly string UserNotFound = "The user cannot be found. Please contact support";
        public static readonly string UserRoleNotValid = "The user role is not valid.";
        public static readonly string UsersNotFound = "No users exist. Please contact support.";
        public static readonly string UserEmailNotValid = "The email for the newly created user is not valid. Please add a valid email.";
        public static readonly string UserPasswordNotValid = "The password for the newly created user is not valid. Please add a valid password.";
        public static readonly string UserAlreadyExists = "The user already exists. Please recheck the data or contact support.";
        public static readonly string ProfessorNotFound = "The professor does not exist. Please contact support.";
        public static readonly string StudentsNotFound = "Cannot load student data. Please contact support.";
        public static readonly string StudentNotFound = "The selected student does not exist. Please contact support.";
        public static readonly string StudentAlreadyGraduated = "The selected student has already graduated. Cannot add a grade to a graduated student.";
        public static readonly string StudentAlreadyGradedForCourse = "The selected student has already been graded for the selected course.";
        public static readonly string AdminDeleteForbidden = "Cannot delete the admin user.";

        // Courses
        public static readonly string CourseNotFound = "The selected course does not exist. Please contact support.";
        public static readonly string ProfessorCourseGradingNotValid = "The selected professor cannot grade the selected course. Please contact support.";
        public static readonly string ProfessorHasCourses = "Cannot delete the professor because they teach at least one course.";

        // Grades
        public static readonly string GradeNotValid = "Cannot submit a grade that is not within the 6-10 range.";
    }
}
