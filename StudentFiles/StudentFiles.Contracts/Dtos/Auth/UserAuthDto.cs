namespace StudentFiles.Contracts.Dtos.Auth
{
    public class UserAuthDto
    {
        public int UserId { get; init; }

        public string Username { get; init; }

        public string HashedPassword { get; init; }
    }
}
