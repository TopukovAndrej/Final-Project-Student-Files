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
        public static readonly string ProfessorNotFound = "The professor does not exist. Please contact support";
    }
}
