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
    }
}
