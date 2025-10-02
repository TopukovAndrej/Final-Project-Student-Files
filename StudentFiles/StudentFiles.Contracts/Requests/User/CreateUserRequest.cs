namespace StudentFiles.Contracts.Requests.User
{
    public class CreateUserRequest
    {
        public string Username { get; }

        public string Password { get; }

        public string Role {  get; }

        public CreateUserRequest(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
