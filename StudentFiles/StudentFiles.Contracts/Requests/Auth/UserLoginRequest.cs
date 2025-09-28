namespace StudentFiles.Contracts.Requests.Auth
{
    public class UserLoginRequest
    {
        public string Username { get; }

        public string Password { get; }

        public UserLoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
