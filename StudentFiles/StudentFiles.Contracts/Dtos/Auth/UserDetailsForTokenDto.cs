namespace StudentFiles.Contracts.Dtos.Auth
{
    public class UserDetailsForTokenDto
    {
        public Guid UserUid { get; init; }

        public string Username { get; init; }

        public string UserRole { get; init; }
    }
}
