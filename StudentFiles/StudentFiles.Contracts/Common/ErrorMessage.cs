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
    }
}
