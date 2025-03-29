namespace StudentFiles.Infrastructure.Data.Models
{
    public class User
    {
        public int Id { get; internal set; }

        public Guid Uid { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public string Username { get; internal set; }

        public string HashedPassword { get; internal set; }

        public string Role { get; internal set; }

        private User() { }

        public User(int id, Guid uid, bool isDeleted, string username, string hashedPassword, string role)
        {
            Id = id;
            Uid = uid;
            IsDeleted = isDeleted;
            Username = username;
            HashedPassword = hashedPassword;
            Role = role;
        }
    }
}
